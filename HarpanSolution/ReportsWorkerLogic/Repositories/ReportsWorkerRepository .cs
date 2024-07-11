using ReportsWorkerLogic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReportsWorkerLogic.Repositories
{
    public class ReportsWorkerRepository : IReportsWorkerRepository
    {
        private readonly List<ReportsWorkerModel> _store = new();

        public Task<IEnumerable<ReportsWorkerModel>> GetAllAsync()
        {
            return Task.FromResult(_store.AsEnumerable());
        }

        public Task<ReportsWorkerModel> GetByIdAsync(int id)
        {
            var model = _store.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(model);
        }

        public Task AddAsync(ReportsWorkerModel model)
        {
            model.Id = _store.Count > 0 ? _store.Max(x => x.Id) + 1 : 1;
            _store.Add(model);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(ReportsWorkerModel model)
        {
            var existingModel = _store.FirstOrDefault(x => x.Id == model.Id);
            if (existingModel != null)
            {
                existingModel.ReportName = model.ReportName;
                existingModel.Content = model.Content;
                existingModel.CreatedDate = model.CreatedDate;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var model = _store.FirstOrDefault(x => x.Id == id);
            if (model != null)
            {
                _store.Remove(model);
            }
            return Task.CompletedTask;
        }
    }
}
