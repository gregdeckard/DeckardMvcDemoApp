using DeckardMvcDemoApp.DataAccess;
using DeckardMvcDemoApp.Interfaces;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DeckardMvcDemoApp.DAL
{
    public class BuildingRepository : IBuildingRepository
    {
        private DatabaseContext _databaseContext;
        private Building _building;

        public BuildingRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _building = new Building();
        }

        public async Task<int> CreateBuilding(Building building)
        {
            var recordsInserted = 0;
            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("CreateBuilding", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).SqlValue = building.Name;
                
                con.Open();
                recordsInserted = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return recordsInserted;
        }

        public async Task<Building> GetBuildings()
        {
            _building = new Building();
            SqlDataReader dataReader = null;

            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetBuildings", con);
                con.Open();

                dataReader = await cmd.ExecuteReaderAsync();
                using (dataReader)
                {
                    while (await dataReader.ReadAsync())
                    {
                        var localBuilding = new Building();
                        localBuilding.Id = (int)dataReader["Id"];
                        localBuilding.Name = (string)dataReader["Name"];

                        _building.Buildings.Add(localBuilding);
                    }
                }

                _building.Buildings = _building.Buildings.OrderBy(x => x.Name).ToList();

                con.Close();
            }

            return _building;
        }

        public async Task<Building> GetBuildingById(int id)
        {
            _building = new Building();
            SqlDataReader dataReader = null;

            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetBuildingById", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).SqlValue = id;
                con.Open();

                dataReader = await cmd.ExecuteReaderAsync();
                using (dataReader)
                {
                    while (await dataReader.ReadAsync())
                    {
                        _building.Id = (int)dataReader["Id"];
                        _building.Name = (string)dataReader["Name"];
                    }
                }

                con.Close();
            }

            return _building;
        }

        public async Task<int> UpdateBuilding(Building building)
        {
            var recordsUpdated = 0;
            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("UpdateBuilding", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@id", SqlDbType.Int).SqlValue = building.Id;
                cmd.Parameters.Add("@name", SqlDbType.NVarChar).SqlValue = building.Name;
                con.Open();
                recordsUpdated = await cmd.ExecuteNonQueryAsync();
                con.Close();
            }

            return recordsUpdated;
        }
    }
}
