namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("LogIn method response", "LogIn method response hold all expected values from server.")]
  public class MethodResponseLogIn : IMethodResponse
  {
    private string token;

    public string Token
    {
      get
      {
        return this.token;
      }
      set
      {
        this.token = value;
      }
    }

    public MethodResponseLogIn()
    {
    }

    public MethodResponseLogIn(string name, string message)
      : base(name, message)
    {
    }
  }
}
