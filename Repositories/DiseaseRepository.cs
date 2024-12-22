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

        // Lấy danh sách theo độ nghiêm trọng
        public async Task<IEnumerable<DiseaseType>> GetBySeverityAsync(string severity)
        {
            return await _context.DiseaseTypes.Where(d => d.Severity == severity).ToListAsync();
        }
    }
}
