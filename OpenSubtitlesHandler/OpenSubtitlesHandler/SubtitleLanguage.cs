namespace OpenSubtitlesHandler
{
  public struct SubtitleLanguage
  {
    private string _SubLanguageID;
    private string _LanguageName;
    private string _ISO639;

    public string SubLanguageID
    {
      get
      {
        return this._SubLanguageID;
      }
      set
      {
        this._SubLanguageID = value;
      }
    }

    public string LanguageName
    {
      get
      {
        return this._LanguageName;
      }
      set
      {
        this._LanguageName = value;
      }
    }

    public string ISO639
    {
      get
      {
        return this._ISO639;
      }
      set
      {
        this._ISO639 = value;
      }
    }

    public override string ToString()
    {
      return this._LanguageName + " [" + this._SubLanguageID + "]";
    }
  }
}
