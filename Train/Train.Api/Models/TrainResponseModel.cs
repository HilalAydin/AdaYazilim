namespace Train.Api.Models
{
    public class TrainResponseModel
    {
        public bool CanBeBooked { get; set; }
        public List<TicketModel> Tickets { get; set; }
    }
}
