using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("GetSubLanguages method response", "GetSubLanguages method response hold all expected values from server.")]
  public class MethodResponseGetSubLanguages : IMethodResponse
  {
    private List<SubtitleLanguage> languages = new List<SubtitleLanguage>();

    public List<SubtitleLanguage> Languages
    {
      get
      {
        return this.languages;
      }
      set
      {
        this.languages = value;
      }
    }

    public MethodResponseGetSubLanguages()
    {
    }

    public MethodResponseGetSubLanguages(string name, string message)
      : base(name, message)
    {
    }
  }
}
