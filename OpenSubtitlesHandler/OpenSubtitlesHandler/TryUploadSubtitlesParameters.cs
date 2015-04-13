namespace OpenSubtitlesHandler
{
  public class TryUploadSubtitlesParameters
  {
    private string _subhash = "";
    private string _subfilename = "";
    private string _moviehash = "";
    private string _moviebytesize = "";
    private string _moviefilename = "";
    private int _movietimems;
    private int _movieframes;
    private double _moviefps;

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

    public override string ToString()
    {
      return this._subfilename + " (" + this._subhash + ")";
    }
  }
}
