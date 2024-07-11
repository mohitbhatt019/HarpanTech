using ReportsServiceLogic.Models;
using ReportsServiceLogic.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsServiceLogic.Helpers
{
    public class ReportsServiceHelper
    {
        private readonly IReportsServiceRepository _repository;

        public ReportsServiceHelper(IReportsServiceRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<ReportsServiceModel>> GetAllReportsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<ReportsServiceModel> GetReportByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task AddReportAsync(ReportsServiceModel model)
        {
            return _repository.AddAsync(model);
        }

        public Task UpdateReportAsync(ReportsServiceModel model)
        {
            return _repository.UpdateAsync(model);
        }

        public Task DeleteReportAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
