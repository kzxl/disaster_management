using disaster_management.Data;
using disaster_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories.Livestock
{
    public class LivestockStatisticRepository : Repository<LivestockStatistic>
    {
        public LivestockStatisticRepository(DaDManagementContext context) : base(context)
        {

        }

        internal async Task<IEnumerable<LivestockStatistic>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
