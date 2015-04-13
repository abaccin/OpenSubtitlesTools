using System.ComponentModel;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("AutoUpdate method response", "AutoUpdate method response hold all expected values from server.")]
  public class MethodResponseAutoUpdate : IMethodResponse
  {
    private string _version;
    private string _url_windows;
    private string _comments;
    private string _url_linux;

    [Category("AutoUpdate")]
    [Description("Latest application version")]
    public string version
    {
      get
      {
        return this._version;
      }
      set
      {
        this._version = value;
      }
    }

    [Category("AutoUpdate")]
    [Description("Download URL for Windows version")]
    public string url_windows
    {
      get
      {
        return this._url_windows;
      }
      set
      {
        this._url_windows = value;
      }
    }

    [Description("Application changelog and other comments")]
    [Category("AutoUpdate")]
    public string comments
    {
      get
      {
        return this._comments;
      }
      set
      {
        this._comments = value;
      }
    }

    [Description("Download URL for Linux version")]
    [Category("AutoUpdate")]
    public string url_linux
    {
      get
      {
        return this._url_linux;
      }
      set
      {
        this._url_linux = value;
      }
    }

    public MethodResponseAutoUpdate()
    {
    }

    public MethodResponseAutoUpdate(string name, string message)
      : base(name, message)
    {
    }
  }
}
