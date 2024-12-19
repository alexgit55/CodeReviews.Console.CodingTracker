using Microsoft.Extensions.Configuration;

namespace CodingTracker.alexgit55
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("C:\\Scripts\\GitHub\\alexgit55\\CodeReviews.Console.CodingTracker\\CodingTracker.alexgit55\\appsettings.json")
            .Build();

            string connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            var dataAccess = new DataAccess(connectionString);

            dataAccess.CreateDatabase();

            UserInterface.MainMenu();
        }
    }
}
