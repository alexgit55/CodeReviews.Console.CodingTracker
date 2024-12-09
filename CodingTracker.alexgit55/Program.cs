using Microsoft.Extensions.Configuration;

namespace CodingTracker.alexgit55
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IConfiguration configuration = new ConfigurationBuilder()
            .AddJsonFile("Configuration\\appsettings.json")
            .Build();

            string connectionString = configuration.GetSection("ConnectionStrings")["DefaultConnection"];

            Console.WriteLine(connectionString);
        }
    }
}
