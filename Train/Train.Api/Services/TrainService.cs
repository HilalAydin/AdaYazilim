using Train.Api.Models;

namespace Train.Api.Services
{
    public class TrainService : ITrainService
    {
        public TrainResponseModel CheckAvailability(TrainRequestModel request)
        {
            var response = new TrainResponseModel
            {
                CanBeBooked = false,
                Tickets = new List<TicketModel>()
            };

            var train = request.Train;

            // If there is no wagon there is no need to check.
            if (train.Wagons.Count <= 0) return response;

            foreach(var wagon in train.Wagons)
            {
                // isSucceeded -> if any placement happened
                (bool isSucceeded, request, TicketModel ticket) = TryToPlaceInWagon(wagon, request);
                if (isSucceeded)
                {
                    response.Tickets.Add(ticket);
                }

                // No need to continue if everyone has a ticket.
                if (request.NumberOfPeople == 0)
                {
                    break;
                }
            }

            // There are still people who need tickets.
            if (request.NumberOfPeople > 0)
            {
                // We are clearing the list because not everyone can book.
                response.Tickets.Clear();
                // Value should be false because not everyone can book.
                response.CanBeBooked = false;
            }
            else
            {
                // Value should be false because everyone can book.
                response.CanBeBooked = true;
            }

            return response;
        }

        private decimal GetRatio(int number, int total)
        {
            return (decimal)Math.Round((double)(100 * number) / total);
        }

        private (bool isSucceeded, TrainRequestModel requestModel, TicketModel ticket) TryToPlaceInWagon(WagonModel wagon, TrainRequestModel request)
        {
            var isSucceeded = false;
            var numberOfPeople = request.NumberOfPeople;
            var ticket = new TicketModel();

            if(!request.CanBePlacedInDifferentWagons)
            {
                var ratio = GetRatio(wagon.Occupancy + request.NumberOfPeople, wagon.Capacity);

                if (ratio < 70)
                {
                    request.NumberOfPeople = 0;
                    isSucceeded = true;
                }
            } else
            {
                for (int index = 1; numberOfPeople >= index; index++)
                {
                    var ratio = GetRatio(wagon.Occupancy + 1, wagon.Capacity);

                    if (ratio < 70)
                    {
                        wagon.Occupancy += 1;
                        request.NumberOfPeople -= 1;
                        isSucceeded = true;
                    }
                    else
                    {
                        break;
                    }
                }
            }

            if (isSucceeded)
            {
                ticket.WagonName = wagon.Name;
                ticket.NumberOfPeople = numberOfPeople - request.NumberOfPeople;
            }

            return (isSucceeded, request, ticket);
        }
    }
}
