namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("InsertMovie method response", "InsertMovie method response hold all expected values from server.")]
  public class MethodResponseInsertMovie : IMethodResponse
  {
    private string _ID;

    public string ID
    {
      get
      {
        return this._ID;
      }
      set
      {
        this._ID = value;
      }
    }

    public MethodResponseInsertMovie()
    {
    }

    public MethodResponseInsertMovie(string name, string message)
      : base(name, message)
    {
    }
  }
}
