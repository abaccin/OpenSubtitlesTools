namespace OpenSubtitlesHandler
{
  public struct GetAvailableTranslationsResult
  {
    private string _language;
    private string _LastCreated;
    private string _StringsNo;

    public string LanguageID
    {
      get
      {
        return this._language;
      }
      set
      {
        this._language = value;
      }
    }

    public string LastCreated
    {
      get
      {
        return this._LastCreated;
      }
      set
      {
        this._LastCreated = value;
      }
    }

    public string StringsNo
    {
      get
      {
        return this._StringsNo;
      }
      set
      {
        this._StringsNo = value;
      }
    }

    public override string ToString()
    {
      return this._language + " (" + this._LastCreated + ")";
    }
  }
}
