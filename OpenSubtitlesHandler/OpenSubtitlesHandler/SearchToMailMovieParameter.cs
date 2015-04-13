namespace OpenSubtitlesHandler
{
  public struct SearchToMailMovieParameter
  {
    private string _moviehash;
    private double _moviesize;

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

    public double moviesize
    {
      get
      {
        return this._moviesize;
      }
      set
      {
        this._moviesize = value;
      }
    }
  }
}
