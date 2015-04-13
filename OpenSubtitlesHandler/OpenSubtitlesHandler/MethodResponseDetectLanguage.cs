using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("DetectLanguage method response", "DetectLanguage method response hold all expected values from server.")]
  public class MethodResponseDetectLanguage : IMethodResponse
  {
    private List<DetectLanguageResult> results = new List<DetectLanguageResult>();

    public List<DetectLanguageResult> Results
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

    public MethodResponseDetectLanguage()
    {
    }

    public MethodResponseDetectLanguage(string name, string message)
      : base(name, message)
    {
    }
  }
}
