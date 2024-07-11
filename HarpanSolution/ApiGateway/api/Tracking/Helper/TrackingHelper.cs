using ApiGateway.api.Tracking.Model;
using ApiGateway.Helpers;

namespace ApiGateway.api.Tracking.Helper
{
    public class TrackingHelper
    {
        string TrakingUrl = "";
        string SecurityCode = "";


        public async Task<TrackingResponseModel> TrackingServiceHelper(TrackingService trackingRequest)
        {
            try
            {
                AzureFunctionHelper azureFunctionHelper = new AzureFunctionHelper(TrakingUrl);
                var responseModel = await azureFunctionHelper.Post<TrackingService, TrackingResponseModel>(trackingRequest);
                return responseModel;
            }
            catch (Exception ex)
            {
                throw;
            }
        }






    }
}
