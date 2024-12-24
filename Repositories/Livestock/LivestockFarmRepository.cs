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
    public class LivestockFarmRepository : Repository<LivestockFarm>
    {
        public LivestockFarmRepository(DbContextOptions<DaDManagementContext> context) : base(context)
        {

        }

        internal async Task<IEnumerable<LivestockFarm>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
