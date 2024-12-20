using Spectre.Console;
using System.Globalization;

namespace CodingTracker.alexgit55;

internal class Validation
{
    internal static DateTime ValidateStartDate(string input)
    {
        DateTime date;
        while (!DateTime.TryParseExact(input, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out date))
        {
            input = AnsiConsole.Ask<string>("\n\nInvalid date. Format: dd-mm-yy hh:mm (24 hour clock). Please try again\n\n");
        }

        return date;
    }

    internal static DateTime ValidateEndDate(DateTime startDate, string endDateInput)
    {
        DateTime endDate;
        while (!DateTime.TryParseExact(endDateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
        {
            endDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: dd-mm-yy hh:mm (24 hour clock). Please try again\n\n");
        }

        while (startDate > endDate)
        {
            endDateInput = AnsiConsole.Ask<string>("\n\nEnd date can't be before start date. Please try again\n\n");

            while (!DateTime.TryParseExact(endDateInput, "dd-MM-yy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out endDate))
            {
                endDateInput = AnsiConsole.Ask<string>("\n\nInvalid date. Format: dd-mm-yy hh:mm (24 hour clock). PLease try again\n\n");
            }
        }

        return endDate;
    }

    internal static bool ValidateSessionExists(IEnumerable<CodingSession> codingSessions, int id)
    {
        CodingSession session = new();

        try
        {
            session = codingSessions.Where(s => s.Id == id).Single();
            return true;
        }
        catch (InvalidOperationException)
        {
            AnsiConsole.MarkupLine($"\nSession with id {id} not found\n");
            return false;
        }
    }
    internal static int GetNumber(string message)
    {
        var number = AnsiConsole.Prompt(
                new TextPrompt<int>(message)
                    .Validate(number =>
                    {
                        if (number < 0)
                            return ValidationResult.Error("[red]Please enter a non-negative number[/]");
                        return ValidationResult.Success();
                    })
        );
        return number;
    }
}
