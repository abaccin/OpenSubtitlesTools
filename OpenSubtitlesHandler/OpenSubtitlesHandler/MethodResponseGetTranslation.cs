namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("GetTranslation method response", "GetTranslation method response hold all expected values from server.")]
  public class MethodResponseGetTranslation : IMethodResponse
  {
    private string data;

    public string ContentData
    {
      get
      {
        return this.data;
      }
      set
      {
        this.data = value;
      }
    }

    public MethodResponseGetTranslation()
    {
    }

    public MethodResponseGetTranslation(string name, string message)
      : base(name, message)
    {
    }
  }
}
