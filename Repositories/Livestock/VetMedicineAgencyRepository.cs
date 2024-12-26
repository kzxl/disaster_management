using disaster_management.Data;
using disaster_management.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories.Livestock
{
    // bác sĩ thú y
    public class VetMedicineAgencyRepository : Repository<VetMedicineAgency>
    {
        public VetMedicineAgencyRepository(DbContextOptions<DaDManagementContext> context) : base(context)
        {

        }

        internal async Task<IEnumerable<VetMedicineAgency>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
