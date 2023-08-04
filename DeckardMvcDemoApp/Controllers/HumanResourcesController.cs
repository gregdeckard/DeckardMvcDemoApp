//using DeckardMvcDemoApp.DataAccess;
using DeckardMvcDemoApp.DAL;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace DeckardMvcDemoApp.Controllers
{
    public class HumanResourcesController : Controller
    {
        private Employee _employee;
        private EmployeeViewModel _employeeViewModel;
        private EmployeeRepository _employeeRepository;

        public HumanResourcesController(EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Building()
        {
            return View();
        }

        public async Task<IActionResult> Employee()
        {
            _employee = new Employee();
            _employee = await _employeeRepository.GetEmployees();
            _employeeViewModel = new EmployeeViewModel();
            foreach (var employee in _employee.Employees)
            {
                _employeeViewModel.Employees.Add(employee);
            }

            return View(_employeeViewModel);
        }

        public async Task<IActionResult> EmployeeCreate([Bind("LastName, FirstName")] Employee employee)
        {
            var numInserted = await _employeeRepository.CreateEmployee(employee);

            return RedirectToAction("Employee");
        }

        public async Task<IActionResult> EmployeeEdit(int id)
        {
            _employeeViewModel = new EmployeeViewModel();

            return View();
        }

        public IActionResult Office()
        {
            return View();
        }
    }
}
