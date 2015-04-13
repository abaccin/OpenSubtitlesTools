namespace OpenSubtitlesHandler
{
  public struct CheckMovieHashResult
  {
    private string name;
    private string _MovieHash;
    private string _MovieImdbID;
    private string _MovieName;
    private string _MovieYear;

    public string MovieHash
    {
      get
      {
        return this._MovieHash;
      }
      set
      {
        this._MovieHash = value;
      }
    }

    public string MovieImdbID
    {
      get
      {
        return this._MovieImdbID;
      }
      set
      {
        this._MovieImdbID = value;
      }
    }

    public string MovieName
    {
      get
      {
        return this._MovieName;
      }
      set
      {
        this._MovieName = value;
      }
    }

    public string MovieYear
    {
      get
      {
        return this._MovieYear;
      }
      set
      {
        this._MovieYear = value;
      }
    }

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public override string ToString()
    {
      return this.name;
    }
  }
}
