using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("GetAvailableTranslations method response", "GetAvailableTranslations method response hold all expected values from server.")]
  public class MethodResponseGetAvailableTranslations : IMethodResponse
  {
    private List<GetAvailableTranslationsResult> results = new List<GetAvailableTranslationsResult>();

    public List<GetAvailableTranslationsResult> Results
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

    public MethodResponseGetAvailableTranslations()
    {
    }

    public MethodResponseGetAvailableTranslations(string name, string message)
      : base(name, message)
    {
    }
  }
}
