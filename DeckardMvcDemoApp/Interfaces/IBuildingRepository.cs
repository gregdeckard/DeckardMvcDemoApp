using DeckardMvcDemoApp.Models;

namespace DeckardMvcDemoApp.Interfaces
{
    public interface IBuildingRepository
    {
        Task<int> CreateBuilding(Building building);
        Task<Building> GetBuildings();
        Task<Building> GetBuildingById(int id);
        Task<int> UpdateBuilding(Building building);
    }
}
