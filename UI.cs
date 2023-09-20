namespace XDVote;

public static class UI
{
    private static PollManager _pollManager = new();
    private static string _options = "1. Show existing Polls\n2. Show results in a specific poll\n3. Create a new poll\n4. Add a candidate or make a vote\n5. Exit\n6. Delete a poll\nChoose your option:";
    public static async Task InitializeConsole()
    {
        await _pollManager.LoadSaveFiles();

        var shouldExitProgram = false;
        while (!shouldExitProgram)
        {
            shouldExitProgram = OptionSelection();
        }
        await _pollManager.Save();
        Console.ReadKey(true);
    }
    private static bool OptionSelection()
    {
        var validInput = false;
        while (!validInput)
        {
            try
            {
                var selectedOption = 0;
                validInput = int.TryParse(ReadInputFromRequest(_options), out selectedOption);
                Console.Clear();
                switch (selectedOption)
                {
                    case 1:
                        ShowListOfPolls();
                        break;
                    case 2:
                        ShowResultsInPoll();
                        break;
                    case 3:
                        var pollName = ReadInputFromRequest("Enter poll name");
                        _pollManager.CreatePoll(pollName);
                        break;
                    case 4:
                        _pollManager.VoteInPoll(SelectPoll(), ReadInputFromRequest("Enter candidate name"));
                        break;
                    case 5:
                        return true;
                    case 6:
                        _pollManager.DeletePoll(SelectPoll());
                        break;
                    default:
                        Console.WriteLine("XD");
                        break;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        return false;
    }
    private static int SelectPoll()
    {
        while (true)
        {
            if (int.TryParse(ReadInputFromRequest("Choose a specific poll"), out int pollIndex))
            {
                return pollIndex;
            }
        }
    }
    private static void ShowListOfPolls()
    {
        var polls = _pollManager.Polls;
        Console.WriteLine("Current list of polls:");
        if (!polls.Any()) Console.WriteLine("Empty");
        foreach (var poll in polls)
        {
            Console.WriteLine(poll.PollName);
        }
        Console.WriteLine("");
    }
    private static void ShowResultsInPoll()
    {
        var index = SelectPoll();
        var candidates = _pollManager.GetCandidatesInPoll(index);

        if (_pollManager.Polls.Any())
        {
            Console.WriteLine($"Current results of voting:");
            foreach (var candidate in candidates)
            {
                Console.WriteLine($"Candidate name:\t{candidate.Name}\tNumber of Votes:\t{candidate.NumberOfVotes}");
            }
            if (!candidates.Any()) Console.WriteLine("This poll is empty");
        }
        else
        {
            Console.WriteLine("No polls availible");
        }
    }
    private static string ReadInputFromRequest(string request)
    {
        Console.WriteLine($"{request}");

        string? input;
        while (true)
        {
            try
            {
                input = Console.ReadLine();
                if (input != null && input != "")
                    return input;
                else
                    throw new ArgumentNullException("Invalid input");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }
    }
}