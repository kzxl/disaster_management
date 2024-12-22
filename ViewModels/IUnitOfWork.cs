using disaster_management.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.ViewModels
{
    public interface IUnitOfWork
    {
        IDiseaseService Diseases { get; }
        Task<int> SaveChangesAsync();
    }
}
