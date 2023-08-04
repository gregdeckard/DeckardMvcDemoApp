using DeckardMvcDemoApp.Models;
using System.Collections;

namespace DeckardMvcDemoApp.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<Employee> GetEmployees();
        Employee GetEmployeeById(int id);
    }
}
