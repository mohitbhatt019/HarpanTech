using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrackingLogic.Models;

namespace TrackingLogic.Helpers
{
    public interface ITrackingRepository
    {
        Task<IEnumerable<TrackingModel>> GetAllAsync();
        Task<TrackingModel> GetByIdAsync(int id);
        Task AddAsync(TrackingModel trackingModel);
        Task UpdateAsync(TrackingModel trackingModel);
        Task DeleteAsync(int id);
    }
}
