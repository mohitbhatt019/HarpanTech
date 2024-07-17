using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReportsServiceApi.Domain.Entities;
using ReportsServiceApi.Helpers;

namespace ReportsServiceApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportServiceController : ControllerBase
    {
        private readonly ILogger _logger;
        private readonly ReportsServiceHelper _reportsServiceHelper;
        public ReportServiceController(ILoggerFactory loggerFactory, ReportsServiceHelper reportsServiceHelper)
        {
            _logger = loggerFactory.CreateLogger<ReportServiceController>();
            _reportsServiceHelper = reportsServiceHelper;
        }

        [HttpGet("GetAllReports")]
        public async Task<IActionResult> GetAllReports()
        {
            _logger.LogInformation("Getting all reports.");

            var reports = await _reportsServiceHelper.GetAllReportsAsync();
            if(reports==null) { return  NotFound(); }

            return Ok(reports);
        }

        [HttpGet("GetReportById")]
        public async Task<IActionResult> GetReportById(int reportId)
        {
            _logger.LogInformation($"Getting report with ID {reportId}.");
            if(reportId == 0) { return NotFound(); }

            var report = await _reportsServiceHelper.GetReportByIdAsync(reportId);

            if (report == null)
            {
                return NotFound();
            }

            return Ok(report);
        }


        [HttpPost("CreateReport")]
        public async Task<IActionResult> CreateReport(Reports reports)
        {
            _logger.LogInformation("Creating a new report.");

            if (reports == null)
            {
                return BadRequest(); ;
            }

            var data =  _reportsServiceHelper.AddReportAsync(reports);


            if (data == null) { return NotFound(); }

            return Ok(data);
        }

        [HttpPut("UpdateReport")]
        public async Task<IActionResult> UpdateReport(Reports reports)
        {
            _logger.LogInformation("Updating a report.");

            if (reports == null || reports.Id == 0)
            {
                return BadRequest(); ;
            }

            var data =  _reportsServiceHelper.UpdateReportAsync(reports);

            if(data == null) { return NotFound(); }

            return Ok(data);
        }

        [HttpDelete("DeleteReport")]
        public async Task<IActionResult> DeleteReport(int reportId)
        {
            _logger.LogInformation($"Deleting report with ID {reportId}.");

            if (reportId == null) return NotFound();

            var data =  _reportsServiceHelper.DeleteReportAsync(reportId);
            if( data == null) { return NotFound(); }
            return Ok(data);
        }

    }
}
