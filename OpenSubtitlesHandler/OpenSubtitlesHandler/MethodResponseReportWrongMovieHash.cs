namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("ReportWrongMovieHash method response", "ReportWrongMovieHash method response hold all expected values from server.")]
  public class MethodResponseReportWrongMovieHash : IMethodResponse
  {
    public MethodResponseReportWrongMovieHash()
    {
    }

    public MethodResponseReportWrongMovieHash(string name, string message)
      : base(name, message)
    {
    }
  }
}
