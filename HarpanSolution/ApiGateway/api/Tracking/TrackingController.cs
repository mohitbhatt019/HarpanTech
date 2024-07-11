using ApiGateway.api.Tracking.Helper;
using ApiGateway.api.Tracking.Model;
using Microsoft.AspNetCore.Mvc;

namespace ApiGateway.api.Tracking
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrackingController : ControllerBase
    {



        [HttpPost]
        [Route("Tracking")]
        public async Task<IActionResult> Tracking(TrackingService trackingRequest)
        {
            TrackingHelper trackingHelper = new TrackingHelper();

            var response = await trackingHelper.TrackingServiceHelper(trackingRequest);

            if (response != null)
                return Ok(response);

            return BadRequest(response);
        }
    }

}

