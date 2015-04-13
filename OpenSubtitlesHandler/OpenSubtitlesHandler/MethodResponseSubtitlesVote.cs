namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("SubtitlesVote method response", "SubtitlesVote method response hold all expected values from server.")]
  public class MethodResponseSubtitlesVote : IMethodResponse
  {
    private string _SubRating;
    private string _SubSumVotes;
    private string _IDSubtitle;

    public string SubRating
    {
      get
      {
        return this._SubRating;
      }
      set
      {
        this._SubRating = value;
      }
    }

    public string SubSumVotes
    {
      get
      {
        return this._SubSumVotes;
      }
      set
      {
        this._SubSumVotes = value;
      }
    }

    public string IDSubtitle
    {
      get
      {
        return this._IDSubtitle;
      }
      set
      {
        this._IDSubtitle = value;
      }
    }

    public MethodResponseSubtitlesVote()
    {
    }

    public MethodResponseSubtitlesVote(string name, string message)
      : base(name, message)
    {
    }
  }
}
