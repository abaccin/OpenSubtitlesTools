using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("GetComments method response", "GetComments method response hold all expected values from server.")]
  public class MethodResponseGetComments : IMethodResponse
  {
    private List<GetCommentsResult> results = new List<GetCommentsResult>();

    public List<GetCommentsResult> Results
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

    public MethodResponseGetComments()
    {
    }

    public MethodResponseGetComments(string name, string message)
      : base(name, message)
    {
    }
  }
}
