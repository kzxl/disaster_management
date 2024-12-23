using disaster_management.Models;
using disaster_management.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace disaster_management.Services
{
    public interface IOutbreakDiagnosisService
    {
        Task<IEnumerable<OutbreakDiagnosis>> GetAllAsync();
        Task<OutbreakDiagnosis?> GetByIdAsync(int id);
        Task AddAsync(OutbreakDiagnosis entity);
        Task UpdateAsync(OutbreakDiagnosis entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<OutbreakDiagnosis>> GetBySeverityAsync(string severity);
        Task<IEnumerable<OutbreakDiagnosis>> GetByNameSearch(string keyword);
    }
    public class OutbreakDiagnosisService : IOutbreakDiagnosisService
    {
        private readonly OutbreakDiagnosisRepository _repository;
        public OutbreakDiagnosisService(OutbreakDiagnosisRepository repository)
        { 
            _repository = repository;
        }

        public async Task<IEnumerable<OutbreakDiagnosis>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<OutbreakDiagnosis?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(OutbreakDiagnosis entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(OutbreakDiagnosis entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public Task<IEnumerable<OutbreakDiagnosis>> GetBySeverityAsync(string severity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OutbreakDiagnosis>> GetByNameSearch(string keyword)
        {
            throw new NotImplementedException();
        }

       

    }
}
