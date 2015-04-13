using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("SubtitleDownload method response", "SubtitleDownload method response hold all expected values from server.")]
  public class MethodResponseSubtitleDownload : IMethodResponse
  {
    private List<SubtitleDownloadResult> results;

    public List<SubtitleDownloadResult> Results
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

    public MethodResponseSubtitleDownload()
    {
      this.results = new List<SubtitleDownloadResult>();
    }

    public MethodResponseSubtitleDownload(string name, string message)
      : base(name, message)
    {
      this.results = new List<SubtitleDownloadResult>();
    }
  }
}
