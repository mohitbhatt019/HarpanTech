using System.Net;
using System.Text.Json;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ReportsWorkerLogic.Helpers;
using ReportsWorkerLogic.Models;

namespace ReportsWorkerServiceApp
{
    public class Function1
    {
        private readonly ILogger _logger;
        private readonly ReportsWorkerHelper _reportsWorkerHelper;

        public Function1(ILoggerFactory loggerFactory, ReportsWorkerHelper reportsWorkerHelper)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
            _reportsWorkerHelper = reportsWorkerHelper;
        }

        [Function("GetAllReports")]
        public async Task<HttpResponseData> GetAllReports([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req)
        {
            _logger.LogInformation("Getting all reports.");

            var reports = await _reportsWorkerHelper.GetAllReportsAsync();

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(reports);

            return response;
        }

        [Function("GetReportById")]
        public async Task<HttpResponseData> GetReportById([HttpTrigger(AuthorizationLevel.Function, "get")] HttpRequestData req, string id)
        {
            _logger.LogInformation($"Getting report with ID {id}.");

            if (!int.TryParse(id, out var reportId))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid ID format.");
                return badResponse;
            }

            var report = await _reportsWorkerHelper.GetReportByIdAsync(reportId);

            if (report == null)
            {
                return req.CreateResponse(HttpStatusCode.NotFound);
            }

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(report);

            return response;
        }

        [Function("CreateReport")]
        public async Task<HttpResponseData> CreateReport([HttpTrigger(AuthorizationLevel.Function, "post")] HttpRequestData req)
        {
            _logger.LogInformation("Creating a new report.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var reportModel = JsonSerializer.Deserialize<ReportsWorkerModel>(requestBody);

            if (reportModel == null)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid request body.");
                return badResponse;
            }

            await _reportsWorkerHelper.AddReportAsync(reportModel);

            var response = req.CreateResponse(HttpStatusCode.Created);
            await response.WriteAsJsonAsync(reportModel);

            return response;
        }

        [Function("UpdateReport")]
        public async Task<HttpResponseData> UpdateReport([HttpTrigger(AuthorizationLevel.Function, "put")] HttpRequestData req)
        {
            _logger.LogInformation("Updating a report.");

            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var reportModel = JsonSerializer.Deserialize<ReportsWorkerModel>(requestBody);

            if (reportModel == null || reportModel.Id == 0)
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid request body or missing ID.");
                return badResponse;
            }

            await _reportsWorkerHelper.UpdateReportAsync(reportModel);

            var response = req.CreateResponse(HttpStatusCode.OK);
            await response.WriteAsJsonAsync(reportModel);

            return response;
        }

        [Function("DeleteReport")]
        public async Task<HttpResponseData> DeleteReport([HttpTrigger(AuthorizationLevel.Function, "delete")] HttpRequestData req, string id)
        {
            _logger.LogInformation($"Deleting report with ID {id}.");

            if (!int.TryParse(id, out var reportId))
            {
                var badResponse = req.CreateResponse(HttpStatusCode.BadRequest);
                await badResponse.WriteStringAsync("Invalid ID format.");
                return badResponse;
            }

            await _reportsWorkerHelper.DeleteReportAsync(reportId);

            return req.CreateResponse(HttpStatusCode.NoContent);
        }
    }
}
