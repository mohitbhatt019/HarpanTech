using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using TrackingLogic.Helpers;
using TrackingLogic.Models;

namespace TrackingServiceApp
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly TrackingHelper _trackingHelper;

        public Function1(ILoggerFactory loggerFactory, TrackingHelper trackingHelper)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _trackingHelper = trackingHelper;
        }

        [Function("GetAllTrackings")]
        public async Task<HttpResponseData> GetAllTrackings([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Getting all trackings.");

            var trackings = await _trackingHelper.GetAllTrackingsAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(trackings);

            return response;
        }

        [Function("GetTrackingById")]
        public async Task<HttpResponseData> GetTrackingById([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, string id)
        {
            _logger.LogInformation($"Getting tracking with ID {id}.");

            if (!int.TryParse(id, out var trackingId))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid ID format.");
                return badResponse;
            }

            var tracking = await _trackingHelper.GetTrackingByIdAsync(trackingId);

            if (tracking == null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(tracking);

            return response;
        }

        [Function("CreateTracking")]
        public async Task<HttpResponseData> CreateTracking([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Creating a new tracking.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var trackingModel = JsonSerializer.Deserialize<TrackingModel>(requestBody);

            if (trackingModel == null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid request body.");
                return badResponse;
            }

            await _trackingHelper.AddTrackingAsync(trackingModel);

            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(trackingModel);

            return response;
        }

        [Function("UpdateTracking")]
        public async Task<HttpResponseData> UpdateTracking([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            _logger.LogInformation("Updating a tracking.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var trackingModel = JsonSerializer.Deserialize<TrackingModel>(requestBody);

            if (trackingModel == null || trackingModel.Id == 0)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid request body or missing ID.");
                return badResponse;
            }

            await _trackingHelper.UpdateTrackingAsync(trackingModel);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(trackingModel);

            return response;
        }

        [Function("DeleteTracking")]
        public async Task<HttpResponseData> DeleteTracking([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req, string id)
        {
            _logger.LogInformation($"Deleting tracking with ID {id}.");

            if (!int.TryParse(id, out var trackingId))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid ID format.");
                return badResponse;
            }

            await _trackingHelper.DeleteTrackingAsync(trackingId);

            return req.CreateResponse(HttpStatusCode.NoContent);
        }

    }
}
