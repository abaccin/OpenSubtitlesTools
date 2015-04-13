using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("CheckMovieHash2 method response", "CheckMovieHash2 method response hold all expected values from server.")]
  public class MethodResponseCheckMovieHash2 : IMethodResponse
  {
    private List<CheckMovieHash2Result> results = new List<CheckMovieHash2Result>();

    public List<CheckMovieHash2Result> Results
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

    public MethodResponseCheckMovieHash2()
    {
    }

    public MethodResponseCheckMovieHash2(string name, string message)
      : base(name, message)
    {
    }
  }
}
