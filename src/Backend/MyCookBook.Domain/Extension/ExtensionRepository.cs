using Microsoft.Extensions.Configuration;

namespace MyCookBook.Domain.Extension
{
    public static class ExtensionRepository
    {
        public static string GetDatabaseName(this IConfiguration configurationManager) 
        {
            var databaseName = configurationManager.GetConnectionString("DatabaseName");
            return databaseName;
        }

        public static string GetConnectionString(this IConfiguration configurationManager)
        {
            var connection = configurationManager.GetConnectionString("Connection");
            return connection;
        }
    }


}
