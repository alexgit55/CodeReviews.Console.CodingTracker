/* The purpose of this program is to allow some to track their coding sessions.
    The user can add a session, start a session, view all sessions, update a session, 
    and delete a session. The program uses a SQLite database to store the sessions. 
    The program uses the Dapper library to interact with the database. 
    The program uses the Spectre.Console library to create a user-friendly interface. 
    The program uses the Microsoft.Extensions.Configuration library to read the connection string from the appsettings.json file
*/

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
