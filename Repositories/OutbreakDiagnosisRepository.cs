using disaster_management.Data;
using disaster_management.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Repositories
{
    public class OutbreakDiagnosisRepository : Repository<OutbreakDiagnosis>
    {
        private readonly DaDManagementContext _context;
        public OutbreakDiagnosisRepository(DaDManagementContext context) : base(context) 
        {
            _context = context;
        }

    }
}
