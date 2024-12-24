using disaster_management.Data;
using disaster_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories.Livestock
{
    public class LivestockFarmConditionRepository : Repository<LivestockFarmCondition>
    {
        public LivestockFarmConditionRepository(DaDManagementContext context) : base(context)
        {
        }

        internal async Task<IEnumerable<LivestockFarmCondition>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
