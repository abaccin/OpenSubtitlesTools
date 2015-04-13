namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("SearchToMail method response", "SearchToMail method response hold all expected values from server.")]
  public class MethodResponseSearchToMail : IMethodResponse
  {
    public MethodResponseSearchToMail()
    {
    }

    public MethodResponseSearchToMail(string name, string message)
      : base(name, message)
    {
    }
  }
}
