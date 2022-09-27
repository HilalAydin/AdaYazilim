namespace Train.Api.Models
{
    public class TrainModel
    {
        public string Name { get; set; }
        public List<WagonModel> Wagons { get; set; }
    }
}
