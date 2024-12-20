// Purpose: This file contains the UserInterface class which is responsible for displaying the main menu of the application and handling user input.

using Spectre.Console;
using static CodingTracker.alexgit55.Enums;

namespace CodingTracker.alexgit55;

internal class UserInterface
{
    internal static void MainMenu()
    // This method is the main menu of the application. It displays the main menu options and allows the user to select an option.
    // It uses the Spectre.Console library to display the menu options and get user input.
    // The menu options are based on the MainMenuChoices enum defined in the Enums.cs file.
    {
        var isMenuRunning = true;
        var menuMessage = "Press any key to return to the main menu";

        while (isMenuRunning)
        {
            
            DisplayHeader("Welcome to the Coding Tracker!");


            var usersChoice = AnsiConsole.Prompt(
                   new SelectionPrompt<MainMenuChoices>()
                    .Title("What would you like to do?")
                    .AddChoices(
                       MainMenuChoices.AddSession,
                       MainMenuChoices.StartSession,
                       MainMenuChoices.ViewSessions,
                       MainMenuChoices.UpdateSession,
                       MainMenuChoices.DeleteSession,
                       MainMenuChoices.Exit)
                    );

            switch (usersChoice)
            {
                case MainMenuChoices.AddSession:
                    DisplayHeader("Add a Coding Session");
                    AddSession();
                    break;
                case MainMenuChoices.StartSession:
                    DisplayHeader("Start a Coding Session");
                    StartSession();
                    break;
                case MainMenuChoices.ViewSessions:
                    DisplayHeader("View Coding Sessions");
                    var dataAccess = new DataAccess();
                    var sessions = dataAccess.GetAllSessions();
                    ViewSessions(sessions);
                    break;
                case MainMenuChoices.UpdateSession:
                    DisplayHeader("Update a Coding Session");
                    UpdateSession();
                    break;
                case MainMenuChoices.DeleteSession:
                    DisplayHeader("Delete a Coding Session");
                    DeleteSession();
                    break;
                case MainMenuChoices.Exit:
                    isMenuRunning = false;
                    DisplayHeader("Thank you for using the Coding Tracker!");
                    menuMessage = "Goodbye!";
                    break;
            }

            AnsiConsole.MarkupLine($"\n{menuMessage}\n");
            Console.ReadKey();
            Console.Clear();
        }
    }
    
    private static void DisplayHeader(string message)
    // This method displays a header with a message using the Spectre.Console library.
    {
        Console.Clear();
        string divider = new string('-', message.Length);
        AnsiConsole.MarkupLine($"[bold green]{divider}[/]");
        AnsiConsole.MarkupLine($"[bold green]{message}[/]");
        AnsiConsole.MarkupLine($"[bold green]{divider}[/]\n");
    }

    private static DateTime[] GetDateInputs()
    // This method prompts the user to input a start date and an end date for a coding session.
    {
        var startDateInput = AnsiConsole.Ask<string>("Input Start Date with the format: dd-mm-yy hh:mm (24 hour clock): ");

        var startDate = Validation.ValidateStartDate(startDateInput);

        var endDateInput = AnsiConsole.Ask<string>("Input End Date with the format: dd-mm-yy hh:mm (24 hour clock): ");

        var endDate = Validation.ValidateEndDate(startDate, endDateInput);

        return [startDate, endDate];
    }

    private static void DeleteSession()
    // This method allows the user to delete a coding session by entering the session ID.
    // It validates the session ID and prompts the user for confirmation before deleting the session.
    {

        var dataAccess = new DataAccess();
        var sessions = dataAccess.GetAllSessions();
        ViewSessions(sessions);

        var id = Validation.GetNumber("Enter the Id of the session you want to delete. Or enter 0 to return to main menu: ");

        if (id == 0) return;

        if (!Validation.ValidateSessionExists(sessions, id))
        {
            AnsiConsole.MarkupLine($"\n[red]Session with id {id} not found.[/]\n");
            return;
        }

        if (AnsiConsole.Prompt(new ConfirmationPrompt($"Are you sure you want to delete session [green]{id}[/]")))
        {
            dataAccess.DeleteSession(id);
            AnsiConsole.MarkupLine($"\nSession with id {id} has been deleted.\n");
        }
        else
            AnsiConsole.MarkupLine("\n[yellow]No session will be deleted.[/]\n");
    }

    private static void UpdateSession()
    // This method allows the user to update a coding session by entering the session ID.
    // It validates the session ID and promptes the user for confirmation before
    // prompting to enter new start and end dates
    {
        var dataAccess = new DataAccess();
        var sessions = dataAccess.GetAllSessions();
        var session = new CodingSession();
        ViewSessions(sessions);

        var id = Validation.GetNumber("Enter the Id of the session you want to update. Or enter 0 to return to main menu: ");

        if (id == 0) return;

        if (!Validation.ValidateSessionExists(sessions, id))
        {
            AnsiConsole.MarkupLine($"\n[red]Session with id {id} not found.[/]\n");
            return;
        }

        if (AnsiConsole.Prompt(new ConfirmationPrompt($"Are you sure you want to update session [green]{id}[/]?")))
        {
            var dateInputs = GetDateInputs();
            session.Id = id;
            session.DateStart = dateInputs[0];
            session.DateEnd = dateInputs[1];
            session.CalculateDuration();
            dataAccess.UpdateSession(session);
            AnsiConsole.MarkupLine($"\nSession with id {id} has been updated.\n");
        }
        else
            AnsiConsole.MarkupLine("\n[yellow]No session will be updated.[/]\n");
    }

    private static void ViewSessions(IEnumerable<CodingSession> sessions)
    // This method displays a table of coding sessions using the Spectre.Console library.
    {

        var table = new Table();
        table.AddColumn("Id");
        table.AddColumn("Start Date");
        table.AddColumn("End Date");
        table.AddColumn("Duration");

        foreach (var session in sessions)
        {
            table.AddRow(session.Id.ToString(), session.DateStart.ToString(), session.DateEnd.ToString(), $"{TimeSpan.Parse(session.Duration).Hours} hours {TimeSpan.Parse(session.Duration).Minutes} minutes");
        }

        AnsiConsole.Write(table);
    }

    private static void AddSession()
    // This method allows the user to add a new coding session by entering the start and end dates.
    {
        CodingSession session = new();

        var dateInputs = GetDateInputs();
        session.DateStart = dateInputs[0];
        session.DateEnd = dateInputs[1];
        session.CalculateDuration();

        var dataAccess = new DataAccess();
        dataAccess.InsertSession(session);
    }

    private static void StartSession()
    // This method allows the user to start a new live coding session and record the total time
    // to the database afterwards
    {
        AnsiConsole.MarkupLine("This will start a live coding session and will record the total time afterwards.\n");

        if (AnsiConsole.Prompt(new ConfirmationPrompt("Are you ready to star a new session?")))
        {
            var continueSession = true;
            var startTime = DateTime.Now;

            while (continueSession)
            {
                DisplayHeader("Coding Session in Progress");
                continueSession = AnsiConsole.Prompt(new ConfirmationPrompt("Continue coding session?"));
            }

            var endTime = DateTime.Now;

            AnsiConsole.MarkupLine($"Session started at {startTime} and ended at {endTime}.");

            CodingSession session = new()
            {
                DateStart = startTime,
                DateEnd = endTime,
            };
            session.CalculateDuration();

            var dataAccess = new DataAccess();
            dataAccess.InsertSession(session);
        }
        else
            AnsiConsole.MarkupLine("\nNo session will be started.");

    }
}
