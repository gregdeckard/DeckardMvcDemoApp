using DeckardMvcDemoApp.Models;

namespace DeckardMvcDemoApp.Interfaces
{
    public interface IBuildingRepository
    {
        Task<Building> GetBuildings();
        Task<Building> GetBuildingById(int id);
    }
}
