using DeckardMvcDemoApp.Models;

namespace DeckardMvcDemoApp.Interfaces
{
    public interface IHelperRepository
    {
        Task<State> GetStates();
    }
}
