using disaster_management.Data;
using disaster_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories.Location
{
    public class WardsRepository : Repository<Ward>
    {
        public WardsRepository(DaDManagementContext context) : base(context)
        {
        }

        internal async Task<IEnumerable<Ward>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }
    }
}
