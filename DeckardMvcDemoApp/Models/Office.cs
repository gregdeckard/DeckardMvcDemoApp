namespace DeckardMvcDemoApp.Models
{
    public class Office
    {
        public int Id { get; set; }
        public int OfficeNumber { get; set; }
        public int BuildingId { get; set; }
        public List<Office> OfficeList { get; set;} = new List<Office>();
    }
}
