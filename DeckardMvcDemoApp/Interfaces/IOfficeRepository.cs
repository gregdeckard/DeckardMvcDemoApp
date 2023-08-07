using DeckardMvcDemoApp.Models;

namespace DeckardMvcDemoApp.Interfaces
{
    public interface IOfficeRepository
    {
        Task<int> CreateOffice(Office office);
        Task<Office> GetOffices();
        Task<Office> GetOfficeById(int id);
        Task<int> UpdateOffice(Office office);
    }
}
