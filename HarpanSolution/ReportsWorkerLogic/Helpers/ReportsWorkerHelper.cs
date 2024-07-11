using ReportsWorkerLogic.Models;
using ReportsWorkerLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsWorkerLogic.Helpers
{
    public class ReportsWorkerHelper
    {
        private readonly IReportsWorkerRepository _repository;

        public ReportsWorkerHelper(IReportsWorkerRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ReportsWorkerModel>> GetAllReportsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<ReportsWorkerModel> GetReportByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task AddReportAsync(ReportsWorkerModel model)
        {
            return _repository.AddAsync(model);
        }

        public Task UpdateReportAsync(ReportsWorkerModel model)
        {
            return _repository.UpdateAsync(model);
        }

        public Task DeleteReportAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
