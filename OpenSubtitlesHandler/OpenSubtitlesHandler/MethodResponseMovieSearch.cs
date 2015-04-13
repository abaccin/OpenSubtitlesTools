using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("MovieSearch method response", "MovieSearch method response hold all expected values from server.")]
  public class MethodResponseMovieSearch : IMethodResponse
  {
    private List<MovieSearchResult> results;

    public List<MovieSearchResult> Results
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

    public MethodResponseMovieSearch()
    {
      this.results = new List<MovieSearchResult>();
    }

    public MethodResponseMovieSearch(string name, string message)
      : base(name, message)
    {
      this.results = new List<MovieSearchResult>();
    }
  }
}
