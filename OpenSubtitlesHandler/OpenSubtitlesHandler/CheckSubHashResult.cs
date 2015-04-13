namespace OpenSubtitlesHandler
{
  public struct CheckSubHashResult
  {
    private string _hash;
    private string _id;

    public string Hash
    {
      get
      {
        return this._hash;
      }
      set
      {
        this._hash = value;
      }
    }

    public string SubID
    {
      get
      {
        return this._id;
      }
      set
      {
        this._id = value;
      }
    }
  }
}
