using ReportsServiceLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsServiceLogic.Repositories
{
    public interface IReportsServiceRepository
    {
        Task<IEnumerable<ReportsServiceModel>> GetAllAsync();
        Task<ReportsServiceModel> GetByIdAsync(int id);
        Task AddAsync(ReportsServiceModel model);
        Task UpdateAsync(ReportsServiceModel model);
        Task DeleteAsync(int id);
    }
}
