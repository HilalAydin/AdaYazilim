using Train.Api.Models;

namespace Train.Api.Services
{
    public interface ITrainService
    {
        TrainResponseModel CheckAvailability(TrainRequestModel request);
    }
}
