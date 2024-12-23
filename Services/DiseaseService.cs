using disaster_management.DTOs;
using disaster_management.Models;
using disaster_management.Repositories;

namespace disaster_management.Services
{
    public interface IDiseaseService
    {
        Task<IEnumerable<DiseaseType>> GetAllAsync();
        Task<DiseaseType?> GetByIdAsync(int id);
        Task AddAsync(DiseaseType entity);
        Task UpdateAsync(DiseaseType entity);
        Task DeleteAsync(int id);
        Task<IEnumerable<DiseaseType>> GetBySeverityAsync(string severity);

    }

    public class DiseaseService : IDiseaseService
    {
        private readonly DiseaseRepository _repository;
        public DiseaseService(DiseaseRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<DiseaseType>> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<DiseaseType?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddAsync(DiseaseType entity)
        {
            await _repository.AddAsync(entity);
        }

        public async Task UpdateAsync(DiseaseType entity)
        {
            await _repository.UpdateAsync(entity);
        }

        public async Task DeleteAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }

        public async Task<IEnumerable<DiseaseType>> GetBySeverityAsync(string severity)
        {
          return await _repository.GetBySeverityAsync(severity);
        }

      
    }
}
