using Microsoft.Extensions.Configuration;

namespace CodingTracker.alexgit55
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var dataAccess = new DataAccess();

            dataAccess.CreateDatabase();

            UserInterface.MainMenu();
        }
    }
}
