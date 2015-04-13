using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("SubtitleSearch method response", "SubtitleSearch method response hold all expected values from server.")]
  public class MethodResponseSubtitleSearch : IMethodResponse
  {
    private List<SubtitleSearchResult> results;

    public List<SubtitleSearchResult> Results
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

    public MethodResponseSubtitleSearch()
    {
      this.results = new List<SubtitleSearchResult>();
    }

    public MethodResponseSubtitleSearch(string name, string message)
      : base(name, message)
    {
      this.results = new List<SubtitleSearchResult>();
    }
  }
}
