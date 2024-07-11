using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingLogic.Models;

namespace TrackingLogic.Helpers
{
    public class TrackingHelper
    {
        private readonly ITrackingRepository _repository;

        public TrackingHelper(ITrackingRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<TrackingModel>> GetAllTrackingsAsync()
        {
            return _repository.GetAllAsync();
        }

        public Task<TrackingModel> GetTrackingByIdAsync(int id)
        {
            return _repository.GetByIdAsync(id);
        }

        public Task AddTrackingAsync(TrackingModel trackingModel)
        {
            return _repository.AddAsync(trackingModel);
        }

        public Task UpdateTrackingAsync(TrackingModel trackingModel)
        {
            return _repository.UpdateAsync(trackingModel);
        }

        public Task DeleteTrackingAsync(int id)
        {
            return _repository.DeleteAsync(id);
        }
    }
}
