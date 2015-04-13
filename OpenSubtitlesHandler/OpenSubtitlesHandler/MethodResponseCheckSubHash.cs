using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("CheckSubHash method response", "CheckSubHash method response hold all expected values from server.")]
  public class MethodResponseCheckSubHash : IMethodResponse
  {
    private List<CheckSubHashResult> results = new List<CheckSubHashResult>();

    public List<CheckSubHashResult> Results
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

    public MethodResponseCheckSubHash()
    {
    }

    public MethodResponseCheckSubHash(string name, string message)
      : base(name, message)
    {
    }
  }
}
