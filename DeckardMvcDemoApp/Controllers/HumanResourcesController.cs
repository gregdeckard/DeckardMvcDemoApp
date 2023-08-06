//using DeckardMvcDemoApp.DataAccess;
using DeckardMvcDemoApp.DAL;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        public HumanResourcesController(EmployeeRepository employeeRepository, HelperRepository helperRepository)
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
            var localEmployeeViewModel = new EmployeeViewModel();
			localEmployeeViewModel = CreateEmployeeViewModel(_employee);
            return View(localEmployeeViewModel);
        }

        public async Task<IActionResult> EmployeeUpdate(int id)
        {
            _employee = new Employee();
            _employee = await _employeeRepository.GetEmployeeById(id);
            var localEmployeeViewModel = new EmployeeViewModel();
            localEmployeeViewModel = CreateEmployeeViewModel(_employee);
            return View(localEmployeeViewModel);
        }

        public async Task<IActionResult> CreateEmployee([Bind("LastName, FirstName")] Employee employee)
        {
            var numInserted = await _employeeRepository.CreateEmployee(employee);
            return RedirectToAction("Employee");
        }

        public async Task<IActionResult> UpdateEmployee([Bind("Id", "LastName", "FirstName")] Employee employee)
        {
            var numInserted = await _employeeRepository.UpdateEmployee(employee);
            return RedirectToAction("Employee");
        }

        private EmployeeViewModel CreateEmployeeViewModel(Employee employeeModel)
        {
			_employeeViewModel = new EmployeeViewModel();

            _employeeViewModel.Id = employeeModel.Id;
            _employeeViewModel.LastName = employeeModel.LastName;
            _employeeViewModel.FirstName = employeeModel.FirstName;

			foreach (var employee in employeeModel.Employees)
			{
				_employeeViewModel.Employees.Add(employee);
			}

			return _employeeViewModel;
        }



        public IActionResult Office()
        {
            return View();
        }
    }
}
