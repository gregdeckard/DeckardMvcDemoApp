using DeckardMvcDemoApp.DataAccess;
using DeckardMvcDemoApp.Interfaces;
using DeckardMvcDemoApp.Models;
using DeckardMvcDemoApp.ViewModels;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.SqlClient;

namespace DeckardMvcDemoApp.DAL
{
    public class HelperRepository : IHelperRepository
    {
        private DatabaseContext _databaseContext;
        private State _state;

        public HelperRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<State> GetStates()
        {
            _state = new State();
            SqlDataReader dataReader = null;

            using (SqlConnection con = new SqlConnection(_databaseContext.GetConnectionString()))
            {
                SqlCommand cmd = new SqlCommand("GetStates", con);
                con.Open();

                dataReader = await cmd.ExecuteReaderAsync();
                using (dataReader)
                {
                    while (await dataReader.ReadAsync())
                    {
                        var localState = new State();
                        localState.Id = (int)dataReader["Id"];
                        localState.StateName = (string)dataReader["State"];
                        localState.StateAbbreviation = (string)dataReader["StateAbbreviation"];

                        _state.States.Add(localState);
                    }
                }

                _state.States = _state.States.OrderBy(x => x.StateName).ToList();

                con.Close();
            }

            return _state;
        }
    }
}
