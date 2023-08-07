using DeckardMvcDemoApp.DataAccess;
using DeckardMvcDemoApp.Interfaces;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DeckardMvcDemoApp.DAL
{
    public class OfficeRepository : IOfficeRepository
    {
        private DatabaseContext _databaseContext;
        private Office _office;

        public OfficeRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<int> CreateOffice(Office office)
        {
            var recordsInserted = 0;
            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CreateOffice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@officeNumber", SqlDbType.NVarChar).SqlValue = office.OfficeNumber;
                cmd.Parameters.Add("@buildingId", SqlDbType.Int).SqlValue = office.BuildingId;

                con.Open();
                recordsInserted = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return recordsInserted;
        }

        public async Task<Office> GetOffices()
        {
            _office = new Office();
            SqlDataReader dataReader = null;

            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetOffices", con);
                con.Open();

                dataReader = await cmd.ExecuteReaderAsync();
                using (dataReader)
                {
                    while (await dataReader.ReadAsync())
                    {
                        var localOffice = new Office();
                        localOffice.Id = (int)dataReader["Id"];
                        localOffice.OfficeNumber = (int)dataReader["OfficeNumber"];
                        localOffice.BuildingId = (int)dataReader["BuildingId"];
                        localOffice.BuildingName = (string)dataReader["Name"];

                        _office.Offices.Add(localOffice);
                    }
                }

                _office.Offices = _office.Offices.OrderBy(x => x.BuildingName).ThenBy(x => x.OfficeNumber).ToList();

                con.Close();
            }

            return _office;
        }

        public async Task<Office> GetOfficeById(int id)
        {
            _office = new Office();
            SqlDataReader dataReader = null;

            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetOfficeById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).SqlValue = id;
                con.Open();

                dataReader = await cmd.ExecuteReaderAsync();
                using (dataReader)
                {
                    while (await dataReader.ReadAsync())
                    {
                        _office.Id = (int)dataReader["Id"];
                        _office.OfficeNumber = (int)dataReader["OfficeNumber"];
                        _office.BuildingId = (int)dataReader["BuildingId"];
                        _office.BuildingName = (string)dataReader["Name"];
                    }
                }

                con.Close();
            }

            return _office;
        }

        public async Task<int> UpdateOffice(Office office)
        {
            var recordsUpdated = 0;
            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateOffice", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).SqlValue = office.Id;
                cmd.Parameters.Add("@officeNumber", SqlDbType.NVarChar).SqlValue = office.OfficeNumber;
                cmd.Parameters.Add("@buildingId", SqlDbType.NVarChar).SqlValue = office.BuildingId;
                con.Open();
                recordsUpdated = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return recordsUpdated;
        }
    }
}
