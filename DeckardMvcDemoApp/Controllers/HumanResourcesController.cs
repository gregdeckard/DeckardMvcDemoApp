using DeckardMvcDemoApp.DAL;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DeckardMvcDemoApp.Controllers
{
    public class HumanResourcesController : Controller
    {
        private Building _building = new Building();
        private BuildingRepository _buildingRepository;
        private Employee _employee = new Employee();
        private EmployeeViewModel _employeeViewModel = new EmployeeViewModel();
        private EmployeeRepository _employeeRepository;
        private Office _office = new Office();
        private OfficeViewModel _officeViewModel = new OfficeViewModel();
        private OfficeRepository _officeRepository;

        public HumanResourcesController(BuildingRepository buildingRepository, EmployeeRepository employeeRepository, OfficeRepository officeRepository)
        {
            _buildingRepository = buildingRepository;
            _employeeRepository = employeeRepository;
            _officeRepository = officeRepository;
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

        public List<SelectListItem> GetBuildingDropdown(Building buildingList)
        {
            List<SelectListItem> localBuildingList = new List<SelectListItem>();
            buildingList.Buildings = buildingList.Buildings.OrderBy(x => x.Name).ToList();
            foreach (var building in buildingList.Buildings)
            {
                var localBuilding = new SelectListItem();
                localBuilding.Value = building.Id.ToString();
                localBuilding.Text = building.Name;
                localBuildingList.Add(localBuilding);
            }
            
            return localBuildingList;
        }

        public BuildingViewModel CreateBuildingViewModel(Building buildingList) 
        { 
            var buildingViewModel = new BuildingViewModel();
            buildingViewModel.Id = buildingList.Id;
            buildingViewModel.Name = buildingList.Name;

            foreach (var building in buildingList.Buildings)
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

        public async Task<IActionResult> Office()
        {
            // Get Office data
            _office = new Office();
            _office = await _officeRepository.GetOffices();
            var localOfficeViewModel = new OfficeViewModel();
            localOfficeViewModel = CreateOfficeViewModel(_office);

            // Get Building data
            _building = new Building();
            _building = await _buildingRepository.GetBuildings();
            var localBuildingList = new List<SelectListItem>();
            localBuildingList = GetBuildingDropdown(_building);
            
            // Add Building data to use in dropdown
            localOfficeViewModel.Buildings = localBuildingList;

            return View(localOfficeViewModel);
        }

        public async Task<IActionResult> OfficeUpdate(int id)
        {
            _office = new Office();
            _office = await _officeRepository.GetOfficeById(id);
            var localOfficeViewModel = new OfficeViewModel();
            localOfficeViewModel = CreateOfficeViewModel(_office);
            return View(localOfficeViewModel);
        }

        public async Task<IActionResult> CreateOffice([Bind("OfficeNumber, BuildingId")] Office office)
        {
            var numInserted = await _officeRepository.CreateOffice(office);
            return RedirectToAction("Office");
        }

        public async Task<IActionResult> UpdateOffice([Bind("Id", "OfficeNumber", "BuildingId")] Office office)
        {
            var numInserted = await _officeRepository.UpdateOffice(office);
            return RedirectToAction("Office");
        }

        public OfficeViewModel CreateOfficeViewModel(Office officeList)
        {
            _officeViewModel = new OfficeViewModel();
            _officeViewModel.Id = officeList.Id;
            _officeViewModel.OfficeNumber = officeList.OfficeNumber;
            _officeViewModel.BuildingId = officeList.BuildingId;
            _officeViewModel.BuildingName = officeList.BuildingName;

            foreach (var office in officeList.Offices)
            {
                var localOffice = new Office();
                localOffice.Id = office.Id;
                localOffice.OfficeNumber = office.OfficeNumber;
                localOffice.BuildingId = office.BuildingId;
                localOffice.BuildingName = office.BuildingName;

                _officeViewModel.Offices.Add(localOffice);
            }

            return _officeViewModel;
        }
    }
}
