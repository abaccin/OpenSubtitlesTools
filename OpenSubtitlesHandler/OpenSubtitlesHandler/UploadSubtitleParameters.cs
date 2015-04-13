namespace OpenSubtitlesHandler
{
  public struct UploadSubtitleParameters
  {
    private string _subhash;
    private string _subfilename;
    private string _moviehash;
    private double _moviebytesize;
    private int _movietimems;
    private int _movieframes;
    private double _moviefps;
    private string _moviefilename;
    private string _subcontent;

    public string subhash
    {
      get
      {
        return this._subhash;
      }
      set
      {
        this._subhash = value;
      }
    }

    public string subfilename
    {
      get
      {
        return this._subfilename;
      }
      set
      {
        this._subfilename = value;
      }
    }

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

    public double moviebytesize
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

    public int movietimems
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

    public int movieframes
    {
      get
      {
        return this._movieframes;
      }
      set
      {
        this._movieframes = value;
      }
    }

    public double moviefps
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

    public string subcontent
    {
      get
      {
        return this._subcontent;
      }
      set
      {
        this._subcontent = value;
      }
    }

    public override string ToString()
    {
      return this._subfilename + " (" + this._subhash + ")";
    }
  }
}
