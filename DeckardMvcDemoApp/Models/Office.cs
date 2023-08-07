namespace DeckardMvcDemoApp.Models
{
    public class Office
    {
        public int Id { get; set; }
        public int OfficeNumber { get; set; }
        public int BuildingId { get; set; }
        public string BuildingName { get; set; }
        public List<Office> Offices { get; set;} = new List<Office>();
    }
}
