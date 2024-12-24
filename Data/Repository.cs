using disaster_management.Models;
using Microsoft.EntityFrameworkCore;

namespace disaster_management.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
      
        private readonly DbContextOptions<DaDManagementContext> _contextOptions;
        public Repository(DbContextOptions<DaDManagementContext> contextOptions)
        {
            _contextOptions = contextOptions;
        }
        private DaDManagementContext CreateContext()
        {
            return new DaDManagementContext(_contextOptions);
        }

        public async Task<IEnumerable<T>> GetAllAsync()
        {
            using var context = CreateContext();
            return await context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            using var context = CreateContext();
            return await context.Set<T>().FindAsync(id);
        }



        public async Task AddAsync(T entity)
        {
            using var context = CreateContext();
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "The entity to add cannot be null.");

            // Nếu thực thể đã được theo dõi, tách nó ra khỏi ChangeTracker
            var trackedEntity = context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Entity == entity);

            if (trackedEntity != null)
            {
                trackedEntity.State = EntityState.Detached; // Tách thực thể đã được theo dõi
            }

            // Đặt giá trị của cột IDENTITY về null hoặc mặc định nếu tồn tại
            var primaryKeyProperty = context.Entry(entity).Properties
                .FirstOrDefault(p => p.Metadata.IsPrimaryKey());

            if (primaryKeyProperty != null && primaryKeyProperty.Metadata.IsPrimaryKey())
            {
                primaryKeyProperty.CurrentValue = null; // Đặt giá trị IDENTITY về null
            }

            // Thêm thực thể mới
            await context.Set<T>().AddAsync(entity);

            // Lưu thay đổi và xử lý ngoại lệ
            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new InvalidOperationException("An error occurred while saving the entity to the database. See inner exception for details.", ex);
            }
        }


        public async Task AddAsync_BK(T entity)
        {
            using var context = CreateContext();
            // Kiểm tra thực thể đã được theo dõi
            var entityType = typeof(T);

            // Lấy tên trường khóa chính
            if (!_primaryKeyFields.TryGetValue(entityType, out var primaryKeyField))
            {
                throw new InvalidOperationException($"Primary key field not defined for type {entityType.Name}.");
            }

            // Lấy giá trị của trường khóa chính từ thực thể
            var primaryKeyValue = context.Entry(entity).Property(primaryKeyField).CurrentValue;

            // Kiểm tra trong ChangeTracker
            var trackedEntity = context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Entity.GetType() == entityType &&
                                     e.Property(primaryKeyField).CurrentValue.Equals(primaryKeyValue));

            if (trackedEntity != null)
            {
                // Tách thực thể đã được theo dõi để tránh xung đột
                context.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }


            await context.Set<T>().AddAsync(entity);
            await context.SaveChangesAsync();
        }

        //Declare the primary keys in each table
        private readonly Dictionary<Type, string> _primaryKeyFields = new()
        {
            { typeof(DiseaseType), "DiseaseId" },
            { typeof(Outbreak), "OutbreakId" },
            { typeof(OutbreakDiagnosis), "DiagnosisId" },
            { typeof(Symptom), "SymptomId" },
            { typeof(Vaccination), "VaccinationId" },
              { typeof(Certificate), "CertificateId" },
        };

        public async Task UpdateAsync(T entity)
        {
            using var context = CreateContext();
            var entityType = typeof(T);

            // Lấy tên trường khóa chính
            if (!_primaryKeyFields.TryGetValue(entityType, out var primaryKeyField))
            {
                throw new InvalidOperationException($"Primary key field not defined for type {entityType.Name}.");
            }

            // Lấy giá trị của khóa chính từ thực thể
            var primaryKeyValue = context.Entry(entity).Property(primaryKeyField).CurrentValue;

            // Tìm thực thể trong DbContext hoặc cơ sở dữ liệu
            var dbEntity = await context.Set<T>().FindAsync(primaryKeyValue);
            if (dbEntity != null)
            {
                // Nếu thực thể tồn tại trong cơ sở dữ liệu, cập nhật giá trị
                context.Entry(dbEntity).CurrentValues.SetValues(entity);
            }
            else
            {
                // Nếu không tìm thấy trong cơ sở dữ liệu, gắn thực thể vào DbContext
                context.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
            }

            // Loại trừ các trường không cần cập nhật
            if (_excludedFields.TryGetValue(entityType, out var excludedFields))
            {
                foreach (var field in excludedFields)
                {
                    var property = context.Entry(entity).Property(field);
                    if (property != null)
                    {
                        property.IsModified = false;
                    }
                }
            }

            // Lưu thay đổi
            await context.SaveChangesAsync();
        }
        public async Task UpdateAsync_Old(T entity)
        {
            using var context = CreateContext();
      
            var entityType = typeof(T);

            if (!_primaryKeyFields.TryGetValue(entityType, out var primaryKeyField))
            {
                throw new InvalidOperationException($"Primary key field not defined for type {entityType.Name}.");
            }
            var trackedEntity = context.ChangeTracker.Entries<T>()
                .FirstOrDefault(e => e.Entity.GetType() == entityType &&
                                     e.Property(primaryKeyField).CurrentValue.Equals(
                                         context.Entry(entity).Property(primaryKeyField).CurrentValue));

            if (trackedEntity != null)
            {
                trackedEntity.CurrentValues.SetValues(entity);
            }
            else
            {
                context.Attach(entity);
            }

            var entry = context.Entry(entity);
            if (_excludedFields.TryGetValue(entityType, out var excludedFields))
            {
                foreach (var field in excludedFields)
                {
                    entry.Property(field).IsModified = false; // Đánh dấu các trường không được cập nhật
                }
            }

            await context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            using var context = CreateContext();
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                context.Set<T>().Remove(entity);
                await context.SaveChangesAsync();
            }
        }

        private readonly Dictionary<Type, string[]> _excludedFields = new()
        {
            { typeof(DiseaseType), new[] { "DiseaseId" } },
          //  { typeof(OtherEntity), new[] { "OtherId", "CreatedDate" } }
        };

    }
}
