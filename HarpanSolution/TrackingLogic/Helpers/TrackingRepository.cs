using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingLogic.Models;

namespace TrackingLogic.Helpers
{
    public class TrackingRepository : ITrackingRepository
    {
        private readonly List<TrackingModel> _trackingModels = new();

        public Task<IEnumerable<TrackingModel>> GetAllAsync()
        {
            return Task.FromResult<IEnumerable<TrackingModel>>(_trackingModels);
        }

        public async Task<TrackingModel>? GetByIdAsync(int id)
        {
            var trackingModel =  _trackingModels.FirstOrDefault(tm => tm.Id == id);
            if (trackingModel == null) return null ;
            return await Task.FromResult(trackingModel);
        }

        public Task AddAsync(TrackingModel trackingModel)
        {
            trackingModel.Id = _trackingModels.Count > 0 ? _trackingModels.Max(tm => tm.Id) + 1 : 1;
            _trackingModels.Add(trackingModel);
            return Task.CompletedTask;
        }

        public Task UpdateAsync(TrackingModel trackingModel)
        {
            var existingModel = _trackingModels.FirstOrDefault(tm => tm.Id == trackingModel.Id);
            if (existingModel != null)
            {
                existingModel.Name = trackingModel.Name;
                existingModel.Item = trackingModel.Item;
                existingModel.DateTime = trackingModel.DateTime;
            }
            return Task.CompletedTask;
        }

        public Task DeleteAsync(int id)
        {
            var trackingModel = _trackingModels.FirstOrDefault(tm => tm.Id == id);
            if (trackingModel != null)
            {
                _trackingModels.Remove(trackingModel);
            }
            return Task.CompletedTask;
        }
    }
}
