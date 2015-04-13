using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("CheckMovieHash method response", "CheckMovieHash method response hold all expected values from server.")]
  public class MethodResponseCheckMovieHash : IMethodResponse
  {
    private List<CheckMovieHashResult> results = new List<CheckMovieHashResult>();

    public List<CheckMovieHashResult> Results
    {
      get
      {
        return this.results;
      }
      set
      {
        this.results = value;
      }
    }

    public MethodResponseCheckMovieHash()
    {
    }

    public MethodResponseCheckMovieHash(string name, string message)
      : base(name, message)
    {
    }
  }
}
