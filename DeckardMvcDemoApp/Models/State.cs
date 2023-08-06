namespace DeckardMvcDemoApp.Models
{
    public class State
    {
        public int Id { get; set; }
        public string? StateName { get; set; }
        public string? StateAbbreviation { get; set; }
        public List<State> States { get; set; } = new List<State>();
    }
}
