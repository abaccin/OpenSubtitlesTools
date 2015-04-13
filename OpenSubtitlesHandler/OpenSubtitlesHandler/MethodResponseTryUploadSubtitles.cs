using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("TryUploadSubtitles method response", "TryUploadSubtitles method response hold all expected values from server.")]
  public class MethodResponseTryUploadSubtitles : IMethodResponse
  {
    private List<SubtitleSearchResult> results = new List<SubtitleSearchResult>();
    private int alreadyindb;

    public int AlreadyInDB
    {
      get
      {
        return this.alreadyindb;
      }
      set
      {
        this.alreadyindb = value;
      }
    }

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

    public MethodResponseTryUploadSubtitles()
    {
    }

    public MethodResponseTryUploadSubtitles(string name, string message)
      : base(name, message)
    {
    }
  }
}
