namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("ReportWrongImdbMovie method response", "ReportWrongImdbMovie method response hold all expected values from server.")]
  public class MethodResponseReportWrongImdbMovie : IMethodResponse
  {
    public MethodResponseReportWrongImdbMovie()
    {
    }

    public MethodResponseReportWrongImdbMovie(string name, string message)
      : base(name, message)
    {
    }
  }
}
