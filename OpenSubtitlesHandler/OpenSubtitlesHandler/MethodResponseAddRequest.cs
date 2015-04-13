namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("AddRequest method response", "AddRequest method response hold all expected values from server.")]
  public class MethodResponseAddRequest : IMethodResponse
  {
    private string _request_url;

    public string request_url
    {
      get
      {
        return this._request_url;
      }
      set
      {
        this._request_url = value;
      }
    }

    public MethodResponseAddRequest()
    {
    }

    public MethodResponseAddRequest(string name, string message)
      : base(name, message)
    {
    }
  }
}
