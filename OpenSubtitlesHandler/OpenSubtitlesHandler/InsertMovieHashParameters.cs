namespace OpenSubtitlesHandler
{
  public class InsertMovieHashParameters
  {
    private string _moviehash = "";
    private string _moviebytesize = "";
    private string _imdbid = "";
    private string _movietimems = "";
    private string _moviefps = "";
    private string _moviefilename = "";

    public string moviehash
    {
      get
      {
        return this._moviehash;
      }
      set
      {
        this._moviehash = value;
      }
    }

    public string moviebytesize
    {
      get
      {
        return this._moviebytesize;
      }
      set
      {
        this._moviebytesize = value;
      }
    }

    public string imdbid
    {
      get
      {
        return this._imdbid;
      }
      set
      {
        this._imdbid = value;
      }
    }

    public string movietimems
    {
      get
      {
        return this._movietimems;
      }
      set
      {
        this._movietimems = value;
      }
    }

    public string moviefps
    {
      get
      {
        return this._moviefps;
      }
      set
      {
        this._moviefps = value;
      }
    }

    public string moviefilename
    {
      get
      {
        return this._moviefilename;
      }
      set
      {
        this._moviefilename = value;
      }
    }
  }
}
