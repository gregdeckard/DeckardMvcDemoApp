namespace DeckardMvcDemoApp.Models
{
    public class Building
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Building> Buildings { get; set; } = new List<Building>();
    }
}
