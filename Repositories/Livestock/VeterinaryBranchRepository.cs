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
    //Chi nhánh thú y
    public class VeterinaryBranchRepository : Repository<VeterinaryBranch>
    {
        public VeterinaryBranchRepository(DbContextOptions<DaDManagementContext> context) : base(context)
        {
        }

        internal async Task<IEnumerable<VeterinaryBranch>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}

// Slaughter house