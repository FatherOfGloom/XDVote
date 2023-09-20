
namespace XDVote;

public class Poll
{
    public Poll(string pollName)
    {
        Candidates = new List<Candidate>();
        PollName = pollName;
    }
    public string PollName { get; }
    public List<Candidate> Candidates { get; set; }
    public void VoteForCandidate(string candidateName)
    {
        var formattedName = candidateName.Trim().ToLower();
        if (!Candidates.Any(x => x.Name == formattedName))
        {
            Candidates.Add(new(formattedName));
        }
        else
        {
            Candidates.Find(x => x.Name == formattedName).Vote();
        }
    }
    public void DeleteCandidate()
    {
        throw new NotImplementedException();
    }

    public class Candidate : ICandidate
    {

        public string Name { get; }
        public int NumberOfVotes
        {
            get { return _numberOfVotes; }
            set { _numberOfVotes = value; }
        }
        private int _numberOfVotes;

        public Candidate(string name)
        {
            Name = name;
            _numberOfVotes = 0;
        }

        public void Vote() => _numberOfVotes++;
    }
}