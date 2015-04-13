namespace OpenSubtitlesHandler
{
  public struct GetCommentsResult
  {
    private string _IDSubtitle;
    private string _UserID;
    private string _UserNickName;
    private string _Comment;
    private string _Created;

    public string IDSubtitle
    {
      get
      {
        return this._IDSubtitle;
      }
      set
      {
        this._IDSubtitle = value;
      }
    }

    public string UserID
    {
      get
      {
        return this._UserID;
      }
      set
      {
        this._UserID = value;
      }
    }

    public string UserNickName
    {
      get
      {
        return this._UserNickName;
      }
      set
      {
        this._UserNickName = value;
      }
    }

    public string Comment
    {
      get
      {
        return this._Comment;
      }
      set
      {
        this._Comment = value;
      }
    }

    public string Created
    {
      get
      {
        return this._Created;
      }
      set
      {
        this._Created = value;
      }
    }
  }
}
