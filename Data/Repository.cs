using disaster_management.Models;
using Microsoft.EntityFrameworkCore;

namespace disaster_management.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DaDManagementContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(DaDManagementContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }



        public async Task AddAsync(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity to add cannot be null.");

            // Nếu thực thể đã được theo dõi, tách nó ra khỏi ChangeTracker
            var trackedEntity = _context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Entity == entity);

            if (trackedEntity != null)
            {
                trackedEntity.State = EntityState.Detached; // Tách thực thể đã được theo dõi
            }

            // Đặt giá trị của cột IDENTITY về null hoặc mặc định nếu tồn tại
            var primaryKeyProperty = _context.Entry(entity).Properties
                .FirstOrDefault(p => p.Metadata.IsPrimaryKey());

            if (primaryKeyProperty != null && primaryKeyProperty.Metadata.IsPrimaryKey())
            {
                primaryKeyProperty.CurrentValue = null; // Đặt giá trị IDENTITY về null
            }

            // Thêm thực thể mới
            await _dbSet.AddAsync(entity);

            // Lưu thay đổi và xử lý ngoại lệ
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while saving the entity to the database. See inner exception for details.", ex);
            }
        }


        public async Task AddAsync_BK(T entity)
        {
            // Kiểm tra thực thể đã được theo dõi
            var entityType = typeof(T);

            // Lấy tên trường khóa chính
            if (!_primaryKeyFields.TryGetValue(entityType, out var primaryKeyField))
            {
                throw new InvalidOperationException($"Primary key field not defined for type {entityType.Name}.");
            }

            // Lấy giá trị của trường khóa chính từ thực thể
            var primaryKeyValue = _context.Entry(entity).Property(primaryKeyField).CurrentValue;

            // Kiểm tra trong ChangeTracker
            var trackedEntity = _context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Entity.GetType() == entityType &&
                                     e.Property(primaryKeyField).CurrentValue.Equals(primaryKeyValue));

            if (trackedEntity != null)
            {
                // Tách thực thể đã được theo dõi để tránh xung đột
                _context.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }


            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        // khai báo danh các khoá chính ở mỗi bảng

        private readonly Dictionary<Type, string> _primaryKeyFields = new()
{
    { typeof(DiseaseType), "DiseaseId" },
  //  { //typeof(OtherEntity), "OtherId" }
};
        public async Task UpdateAsync(T entity)
        {
           
            // Kiểm tra thực thể đã được theo dõi
            var entityType = typeof(T);

            // Lấy tên trường khóa chính
            if (!_primaryKeyFields.TryGetValue(entityType, out var primaryKeyField))
            {
                throw new InvalidOperationException($"Primary key field not defined for type {entityType.Name}.");
            }
            var trackedEntity = _context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Entity.GetType() == entityType &&
                                     e.Property(primaryKeyField).CurrentValue.Equals(
                                         _context.Entry(entity).Property(primaryKeyField).CurrentValue));

            if (trackedEntity != null)
            {
                // Nếu thực thể đã được theo dõi, cập nhật giá trị
                trackedEntity.CurrentValues.SetValues(entity);
            }
            else
            {
                // Nếu chưa được theo dõi, đính kèm thực thể
                _context.Attach(entity);
            }

            // Lấy trạng thái của thực thể
            var entry = _context.Entry(entity);
            if (_excludedFields.TryGetValue(entityType, out var excludedFields))
            {
                foreach (var field in excludedFields)
                {
                    entry.Property(field).IsModified = false; // Đánh dấu các trường không được cập nhật
                }
            }

            // Lưu thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        private readonly Dictionary<Type, string[]> _excludedFields = new()
        {
            { typeof(DiseaseType), new[] { "DiseaseId" } },
          //  { typeof(OtherEntity), new[] { "OtherId", "CreatedDate" } }
        };

    }
}
