using disaster_management.Data;
using disaster_management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories
{

    public class DiseaseRepository : Repository<DiseaseType>
    {
        private readonly DaDManagementContext _context;
        public DiseaseRepository(DaDManagementContext context) : base(context)
        {
            _context = context;
        }

        // Get the list by severity
        public async Task<IEnumerable<DiseaseType>> GetBySeverityAsync(string severity)
        {
            return await _context.DiseaseTypes.Where(d => d.Severity == severity).ToListAsync();
        }

        public async Task<IEnumerable<DiseaseType>> GetByNameSearch(string keyword)
        {
            if (string.IsNullOrWhiteSpace(keyword))
            {
                // Trả về toàn bộ dữ liệu nếu không có từ khóa tìm kiếm
                return await _context.DiseaseTypes.ToListAsync();
            }

            // Tìm kiếm dựa trên từ khóa
            return await _context.DiseaseTypes
                .Where(d => EF.Functions.Like(d.DiseaseName, $"%{keyword}%"))
                .ToListAsync();
        }
    }
}
