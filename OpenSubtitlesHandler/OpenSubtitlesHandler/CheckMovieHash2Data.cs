namespace OpenSubtitlesHandler
{
  public struct CheckMovieHash2Data
  {
    private string _MovieHash;
    private string _MovieImdbID;
    private string _MovieName;
    private string _MovieYear;
    private string _MovieKind;
    private string _SeriesSeason;
    private string _SeriesEpisode;
    private string _SeenCount;

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

    public string MovieKind
    {
      get
      {
        return this._MovieKind;
      }
      set
      {
        this._MovieKind = value;
      }
    }

    public string SeriesSeason
    {
      get
      {
        return this._SeriesSeason;
      }
      set
      {
        this._SeriesSeason = value;
      }
    }

    public string SeriesEpisode
    {
      get
      {
        return this._SeriesEpisode;
      }
      set
      {
        this._SeriesEpisode = value;
      }
    }

    public string SeenCount
    {
      get
      {
        return this._SeenCount;
      }
      set
      {
        this._SeenCount = value;
      }
    }
  }
}
