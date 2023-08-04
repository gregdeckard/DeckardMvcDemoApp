using Microsoft.IdentityModel.Tokens;

namespace DeckardMvcDemoApp.DataAccess
{
    public class DatabaseContext
    {
        private readonly string _connectionString = "Server=(localdb)\\ProjectModels;Database=DeckardMvcDemoAppDatabase;Trusted_Connection=True;MultipleActiveResultSets=true";
        
        public string GetConnectionString()
        {
            return this._connectionString;
        }
    }
}
