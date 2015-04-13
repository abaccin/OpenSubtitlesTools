namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("UploadSubtitles method response", "UploadSubtitles method response hold all expected values from server.")]
  public class MethodResponseUploadSubtitles : IMethodResponse
  {
    private string _data;
    private bool _subtitles;

    public string Data
    {
      get
      {
        return this._data;
      }
      set
      {
        this._data = value;
      }
    }

    public bool SubTitles
    {
      get
      {
        return this._subtitles;
      }
      set
      {
        this._subtitles = value;
      }
    }

    public MethodResponseUploadSubtitles()
    {
    }

    public MethodResponseUploadSubtitles(string name, string message)
      : base(name, message)
    {
    }
  }
}
