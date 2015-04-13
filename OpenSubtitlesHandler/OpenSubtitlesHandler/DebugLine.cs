namespace OpenSubtitlesHandler
{
  public struct DebugLine
  {
    private string debugLine;
    private DebugCode status;

    public string Text
    {
      get
      {
        return this.debugLine;
      }
      set
      {
        this.debugLine = value;
      }
    }

    public DebugCode Code
    {
      get
      {
        return this.status;
      }
      set
      {
        this.status = value;
      }
    }

    public DebugLine(string debugLine, DebugCode status)
    {
      this.debugLine = debugLine;
      this.status = status;
    }
  }
}
