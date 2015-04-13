namespace OpenSubtitlesHandler
{
  public struct SubtitleSearchParameters
  {
    private string subLanguageId;
    private string movieHash;
    private double movieByteSize;
    private string imdbid;

    public string SubLangaugeID
    {
      get
      {
        return this.subLanguageId;
      }
      set
      {
        this.subLanguageId = value;
      }
    }

    public string MovieHash
    {
      get
      {
        return this.movieHash;
      }
      set
      {
        this.movieHash = value;
      }
    }

    public double MovieByteSize
    {
      get
      {
        return this.movieByteSize;
      }
      set
      {
        this.movieByteSize = value;
      }
    }

    public string IMDbID
    {
      get
      {
        return this.imdbid;
      }
      set
      {
        this.imdbid = value;
      }
    }

    public SubtitleSearchParameters(string subLanguageId, string movieHash, int movieByteSize)
    {
      this.subLanguageId = subLanguageId;
      this.movieHash = movieHash;
      this.movieByteSize = movieByteSize;
      this.imdbid = "";
    }

    public SubtitleSearchParameters(string subLanguageId, string movieHash, int movieByteSize, string imdbid)
    {
      this.subLanguageId = subLanguageId;
      this.movieHash = movieHash;
      this.movieByteSize = movieByteSize;
      this.imdbid = imdbid;
    }
  }
}
