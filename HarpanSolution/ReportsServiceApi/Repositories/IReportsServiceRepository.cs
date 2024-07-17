using ReportsServiceApi.Domain.Entities;

namespace ReportsServiceApi.Repositories
{
    public interface IReportsServiceRepository
    {
        Task<IEnumerable<Reports>> GetAllAsync();
        Task<Reports> GetByIdAsync(int id);
        Task AddAsync(Reports model);
        Task UpdateAsync(Reports model);
        Task DeleteAsync(int id);
    }
}
