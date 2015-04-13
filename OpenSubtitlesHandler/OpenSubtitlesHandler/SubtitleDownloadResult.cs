namespace OpenSubtitlesHandler
{
  public struct SubtitleDownloadResult
  {
    private string idsubtitlefile;
    private string data;

    public string IdSubtitleFile
    {
      get
      {
        return this.idsubtitlefile;
      }
      set
      {
        this.idsubtitlefile = value;
      }
    }

    public string Data
    {
      get
      {
        return this.data;
      }
      set
      {
        this.data = value;
      }
    }

    public override string ToString()
    {
      return ((object) this.idsubtitlefile).ToString();
    }
  }
}
