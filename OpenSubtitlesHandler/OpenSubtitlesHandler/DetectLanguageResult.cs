namespace OpenSubtitlesHandler
{
  public struct DetectLanguageResult
  {
    private string inputSample;
    private string languageID;

    public string LanguageID
    {
      get
      {
        return this.languageID;
      }
      set
      {
        this.languageID = value;
      }
    }

    public string InputSample
    {
      get
      {
        return this.inputSample;
      }
      set
      {
        this.inputSample = value;
      }
    }
  }
}
