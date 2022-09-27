namespace Train.Api.Models
{
    public class TrainRequestModel
    {
        public TrainModel Train { get; set; }
        public int NumberOfPeople { get; set; }
        public bool CanBePlacedInDifferentWagons { get; set; }
    }
}
