using ReportsServiceApi.Domain.Entities;
using ReportsServiceApi.Repositories;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsServiceApi.Helpers
{
    public class ReportsServiceHelper
    {
        private readonly IReportsServiceRepository _repository;

        public ReportsServiceHelper(IReportsServiceRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Reports>> GetAllReportsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<Reports> GetReportByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task AddReportAsync(Reports model)
        {
            return _repository.AddAsync(model);
        }

        public Task UpdateReportAsync(Reports model)
        {
            return _repository.UpdateAsync(model);
        }

        public Task DeleteReportAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
