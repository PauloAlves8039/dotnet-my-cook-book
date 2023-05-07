using Dapper;
using MySqlConnector;

namespace MyCookBook.Infrastructure.Migrations
{
    public static class Database
    {
        public static void CreateDatabase(string databaseConnection, string databaseName) 
        {
            using var myConnection = new MySqlConnection(databaseConnection);

            var parameters = new DynamicParameters(databaseName);
            parameters.Add("name", databaseName);

            var registries = myConnection.Query("SELECT * FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME = @name", parameters);

            if (!registries.Any()) 
            {
                myConnection.Execute($"CREATE DATABASE {databaseName}");
            }
        }
    }
}
