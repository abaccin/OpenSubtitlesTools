namespace OpenSubtitlesHandler
{
  public struct SubtitleSearchResult
  {
    private string _IDSubMovieFile;
    private string _MovieHash;
    private string _MovieByteSize;
    private string _MovieTimeMS;
    private string _IDSubtitleFile;
    private string _SubFileName;
    private string _SubActualCD;
    private string _SubSize;
    private string _SubHash;
    private string _IDSubtitle;
    private string _UserID;
    private string _SubLanguageID;
    private string _SubFormat;
    private string _SubSumCD;
    private string _SubAuthorComment;
    private string _SubAddDate;
    private string _SubBad;
    private string _SubRating;
    private string _SubDownloadsCnt;
    private string _MovieReleaseName;
    private string _IDMovie;
    private string _IDMovieImdb;
    private string _MovieName;
    private string _MovieNameEng;
    private string _MovieYear;
    private string _MovieImdbRating;
    private string _UserNickName;
    private string _ISO639;
    private string _LanguageName;
    private string _SubDownloadLink;
    private string _ZipDownloadLink;

    public string IDSubMovieFile
    {
      get
      {
        return this._IDSubMovieFile;
      }
      set
      {
        this._IDSubMovieFile = value;
      }
    }

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

    public string MovieByteSize
    {
      get
      {
        return this._MovieByteSize;
      }
      set
      {
        this._MovieByteSize = value;
      }
    }

    public string MovieTimeMS
    {
      get
      {
        return this._MovieTimeMS;
      }
      set
      {
        this._MovieTimeMS = value;
      }
    }

    public string IDSubtitleFile
    {
      get
      {
        return this._IDSubtitleFile;
      }
      set
      {
        this._IDSubtitleFile = value;
      }
    }

    public string SubFileName
    {
      get
      {
        return this._SubFileName;
      }
      set
      {
        this._SubFileName = value;
      }
    }

    public string SubActualCD
    {
      get
      {
        return this._SubActualCD;
      }
      set
      {
        this._SubActualCD = value;
      }
    }

    public string SubSize
    {
      get
      {
        return this._SubSize;
      }
      set
      {
        this._SubSize = value;
      }
    }

    public string SubHash
    {
      get
      {
        return this._SubHash;
      }
      set
      {
        this._SubHash = value;
      }
    }

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

    public string SubFormat
    {
      get
      {
        return this._SubFormat;
      }
      set
      {
        this._SubFormat = value;
      }
    }

    public string SubSumCD
    {
      get
      {
        return this._SubSumCD;
      }
      set
      {
        this._SubSumCD = value;
      }
    }

    public string SubAuthorComment
    {
      get
      {
        return this._SubAuthorComment;
      }
      set
      {
        this._SubAuthorComment = value;
      }
    }

    public string SubAddDate
    {
      get
      {
        return this._SubAddDate;
      }
      set
      {
        this._SubAddDate = value;
      }
    }

    public string SubBad
    {
      get
      {
        return this._SubBad;
      }
      set
      {
        this._SubBad = value;
      }
    }

    public string SubRating
    {
      get
      {
        return this._SubRating;
      }
      set
      {
        this._SubRating = value;
      }
    }

    public string SubDownloadsCnt
    {
      get
      {
        return this._SubDownloadsCnt;
      }
      set
      {
        this._SubDownloadsCnt = value;
      }
    }

    public string MovieReleaseName
    {
      get
      {
        return this._MovieReleaseName;
      }
      set
      {
        this._MovieReleaseName = value;
      }
    }

    public string IDMovie
    {
      get
      {
        return this._IDMovie;
      }
      set
      {
        this._IDMovie = value;
      }
    }

    public string IDMovieImdb
    {
      get
      {
        return this._IDMovieImdb;
      }
      set
      {
        this._IDMovieImdb = value;
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

    public string MovieNameEng
    {
      get
      {
        return this._MovieNameEng;
      }
      set
      {
        this._MovieNameEng = value;
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

    public string MovieImdbRating
    {
      get
      {
        return this._MovieImdbRating;
      }
      set
      {
        this._MovieImdbRating = value;
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

    public string SubDownloadLink
    {
      get
      {
        return this._SubDownloadLink;
      }
      set
      {
        this._SubDownloadLink = value;
      }
    }

    public string ZipDownloadLink
    {
      get
      {
        return this._ZipDownloadLink;
      }
      set
      {
        this._ZipDownloadLink = value;
      }
    }

    public override string ToString()
    {
      return this._SubFileName + " (" + this._SubFormat + ")";
    }
  }
}
