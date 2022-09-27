using Microsoft.AspNetCore.Mvc;
using Train.Api.Models;
using Train.Api.Services;

namespace Train.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainController : ControllerBase
    {
        private readonly ITrainService _trainService;

        public TrainController(IServiceProvider serviceProvider)
        {
            _trainService = serviceProvider.GetService<ITrainService>()!;
        }

        [HttpPost]
        [ProducesResponseType(typeof(TrainResponseModel), StatusCodes.Status200OK)]
        public IActionResult Index(TrainRequestModel request)
        {
            var response = _trainService.CheckAvailability(request);
            return Ok(response);
        }
    }
}
