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
            _employee = new Employee();
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

        public async Task<Employee> GetEmployeeById(int id)
        {
			_employee = new Employee();
			SqlDataReader dataReader = null;

			using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
			{
				SqlCommand cmd = new SqlCommand("GetEmployeeById", con);
				cmd.CommandType = CommandType.StoredProcedure;
				cmd.Parameters.Add("@id", SqlDbType.Int).SqlValue = id;
				con.Open();

				dataReader = await cmd.ExecuteReaderAsync();
				using (dataReader)
				{
					while (await dataReader.ReadAsync())
					{
						_employee.Id = (int)dataReader["Id"];
						_employee.LastName = (string)dataReader["LastName"];
                        _employee.FirstName = (string)dataReader["FirstName"];
					}
				}

				con.Close();
			}

			return _employee;
		}

        public async Task<int> UpdateEmployee(Employee employee)
        {
            var recordsUpdated = 0;
            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateEmployee", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).SqlValue = employee.Id;
                cmd.Parameters.Add("@lastName", SqlDbType.NVarChar).SqlValue = employee.LastName;
                cmd.Parameters.Add("@firstName", SqlDbType.NVarChar).SqlValue = employee.FirstName;
                con.Open();
                recordsUpdated = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return recordsUpdated;
        }
    }
}
