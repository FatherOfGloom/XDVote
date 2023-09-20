using System.Text.Json;

namespace XDVote;

public class PollManager
{
    private List<Poll> _polls;
    private string _filepath;
    public PollManager()
    {
        _polls = new();
        _filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "polls.json");
    }
    public IEnumerable<Poll> Polls => _polls;

    public void CreatePoll(string pollName)
    {
        _polls.Add(new(pollName));
    }

    public void DeletePoll(int pollIndex)
    {
        _polls.RemoveAt(pollIndex);
    }

    public void VoteInPoll(int pollIndex, string candidateName)
    {
        if (_polls.Any())
        {
            _polls[pollIndex].VoteForCandidate(candidateName);
        }
        else
        {
            throw new ArgumentNullException("No polls available");
        }
    }
    public async Task LoadSaveFiles()
    {
        if (File.Exists(_filepath))
        {
            using (FileStream fileStream = new FileStream(_filepath, FileMode.OpenOrCreate))
            {
                _polls = await JsonSerializer.DeserializeAsync<List<Poll>>(fileStream);
            }
        }
    }
    public async Task Save()
    {
        using (FileStream fileStream = new FileStream(_filepath, FileMode.Create))
        {
            await JsonSerializer.SerializeAsync<List<Poll>>(fileStream, _polls, new JsonSerializerOptions()
            {
                WriteIndented = true
            });
        }
    }
    public IEnumerable<ICandidate> GetCandidatesInPoll(int pollIndex)
    {
        var poll = _polls[pollIndex];
        foreach (var candidate in poll.Candidates)
        {
            yield return candidate;
        }
    }

    public void DeleteCandidateInPoll(int pollIndex, string candidateName)
    {
        if (_polls.Any())
        {
            throw new NotImplementedException();
        }
        else
        {
            throw new ArgumentNullException("No polls available");
        }
    }
}