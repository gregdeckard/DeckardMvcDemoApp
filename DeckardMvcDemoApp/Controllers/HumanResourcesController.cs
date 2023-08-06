using DeckardMvcDemoApp.DAL;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;


namespace DeckardMvcDemoApp.Controllers
{
    public class HumanResourcesController : Controller
    {
        private Building _building;
        private BuildingRepository _buildingRepository;
        private Employee _employee;
        private EmployeeViewModel _employeeViewModel;
        private EmployeeRepository _employeeRepository;

        public HumanResourcesController(BuildingRepository buildingRepository, EmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _buildingRepository = buildingRepository;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> Building()
        {
            _building = new Building();
            _building = await _buildingRepository.GetBuildings();
            var localBuildingViewModel = new BuildingViewModel();
            localBuildingViewModel = CreateBuildingViewModel(_building);
            return View(localBuildingViewModel);
        }

        public async Task<IActionResult> BuildingUpdate(int id)
        {
            _building = new Building();
            _building = await _buildingRepository.GetBuildingById(id);
            var localBuildingViewModel = new BuildingViewModel();
            localBuildingViewModel = CreateBuildingViewModel(_building);
            return View(localBuildingViewModel);
        }

        public async Task<IActionResult> CreateBuilding([Bind("Name")] Building building)
        {
            var numInserted = await _buildingRepository.CreateBuilding(building);
            return RedirectToAction("Building");
        }

        public async Task<IActionResult> UpdateBuilding([Bind("Id", "Name")] Building building)
        {
            var numInserted = await _buildingRepository.UpdateBuilding(building);
            return RedirectToAction("Building");
        }

        public BuildingViewModel CreateBuildingViewModel(Building buildings) 
        { 
            var buildingViewModel = new BuildingViewModel();
            buildingViewModel.Id = buildings.Id;
            buildingViewModel.Name = buildings.Name;

            foreach (var building in buildings.Buildings)
            {
                var localBuilding = new Building();
                localBuilding.Id = building.Id;
                localBuilding.Name = building.Name;
                buildingViewModel.Buildings.Add(localBuilding);
            }

            return buildingViewModel;
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
