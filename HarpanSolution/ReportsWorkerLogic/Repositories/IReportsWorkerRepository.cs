using ReportsWorkerLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsWorkerLogic.Repositories
{
    public interface IReportsWorkerRepository
    {
        Task<IEnumerable<ReportsWorkerModel>> GetAllAsync();
        Task<ReportsWorkerModel> GetByIdAsync(int id);
        Task AddAsync(ReportsWorkerModel model);
        Task UpdateAsync(ReportsWorkerModel model);
        Task DeleteAsync(int id);
    }
}
