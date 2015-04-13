namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("Error method response", "Error method response that describes error that occured")]
  public class MethodResponseError : IMethodResponse
  {
    public MethodResponseError()
    {
    }

    public MethodResponseError(string name, string message)
      : base(name, message)
    {
    }
  }
}
