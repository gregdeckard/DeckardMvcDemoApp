using DeckardMvcDemoApp.DataAccess;
using DeckardMvcDemoApp.Interfaces;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DeckardMvcDemoApp.DAL
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private DatabaseContext _databaseContext;
        private Employee _employee;

        public EmployeeRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public Employee GetEmployeeById(int id)
        {
            _employee = new Employee();
            return _employee;
        }

        public async Task<int> CreateEmployee(Employee employee)
        {
            var recordsInserted = 0;
            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CreateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).SqlValue = employee.LastName;
                cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).SqlValue = employee.FirstName;
                con.Open();
                recordsInserted = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return recordsInserted;
        }

        public async Task<Employee> GetEmployees()
        {
            _employee = new Employee();
            SqlDataReader dataReader = null;

            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetEmployees", con);
                con.Open();

                dataReader = await cmd.ExecuteReaderAsync();
                using (dataReader)
                {
                    while (await dataReader.ReadAsync())
                    {
                        var localEmployee = new Employee();
                        localEmployee.Id = (int)dataReader["Id"];
                        localEmployee.LastName = (string)dataReader["LastName"];
                        localEmployee.FirstName = (string)dataReader["FirstName"];

                        _employee.Employees.Add(localEmployee);
                    }
                }

                _employee.Employees = _employee.Employees.OrderBy(x => x.LastName).ToList();

                con.Close();
            }

            return _employee;
        }
    }
}
