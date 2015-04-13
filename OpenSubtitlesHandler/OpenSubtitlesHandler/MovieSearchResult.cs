namespace OpenSubtitlesHandler
{
  public struct MovieSearchResult
  {
    private string id;
    private string title;

    public string ID
    {
      get
      {
        return this.id;
      }
      set
      {
        this.id = value;
      }
    }

    public string Title
    {
      get
      {
        return this.title;
      }
      set
      {
        this.title = value;
      }
    }

    public override string ToString()
    {
      return this.title;
    }
  }
}
