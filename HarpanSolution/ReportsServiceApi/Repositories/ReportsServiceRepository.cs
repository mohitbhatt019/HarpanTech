

using ReportsServiceApi.Domain.Entities;

namespace ReportsServiceApi.Repositories
{
    public class ReportsServiceRepository : IReportsServiceRepository
    {
        private readonly List<Reports> _store = new();

        public Task<IEnumerable<Reports>> GetAllAsync()
        {
            return Task.FromResult(_store.AsEnumerable());
        }

        public Task<Reports> GetByIdAsync(int id)
        {
            var model = _store.FirstOrDefault(x => x.Id == id);
            return Task.FromResult(model);
        }

        public Task AddAsync(Reports model)
        {
            model.Id = _store.Count > 0 ? _store.Max(x => x.Id) + 1 : 1;
            _store.Add(model);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(Reports model)
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
