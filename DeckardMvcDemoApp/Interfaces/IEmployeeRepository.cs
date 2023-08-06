using DeckardMvcDemoApp.Models;
using System.Collections;

namespace DeckardMvcDemoApp.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<int> CreateEmployee(Employee employee);
        Task<Employee> GetEmployees();
        Task<Employee> GetEmployeeById(int id);
        Task<int> UpdateEmployee(Employee employee);
    }
}
