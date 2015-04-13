namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("AddComment method response", "AddComment method response hold all expected values from server.")]
  public class MethodResponseAddComment : IMethodResponse
  {
    public MethodResponseAddComment()
    {
    }

    public MethodResponseAddComment(string name, string message)
      : base(name, message)
    {
    }
  }
}
