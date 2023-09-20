namespace XDVote;

public interface ICandidate
{
    public string Name { get; }
    public int NumberOfVotes { get; set; }

}