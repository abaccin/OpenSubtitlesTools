using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  public class UploadSubtitleInfoParameters
  {
    private string _idmovieimdb;
    private string _moviereleasename;
    private string _movieaka;
    private string _sublanguageid;
    private string _subauthorcomment;
    private bool _hearingimpaired;
    private bool _highdefinition;
    private bool _automatictranslation;
    private List<UploadSubtitleParameters> cds;

    public string idmovieimdb
    {
      get
      {
        return this._idmovieimdb;
      }
      set
      {
        this._idmovieimdb = value;
      }
    }

    public string moviereleasename
    {
      get
      {
        return this._moviereleasename;
      }
      set
      {
        this._moviereleasename = value;
      }
    }

    public string movieaka
    {
      get
      {
        return this._movieaka;
      }
      set
      {
        this._movieaka = value;
      }
    }

    public string sublanguageid
    {
      get
      {
        return this._sublanguageid;
      }
      set
      {
        this._sublanguageid = value;
      }
    }

    public string subauthorcomment
    {
      get
      {
        return this._subauthorcomment;
      }
      set
      {
        this._subauthorcomment = value;
      }
    }

    public bool hearingimpaired
    {
      get
      {
        return this._hearingimpaired;
      }
      set
      {
        this._hearingimpaired = value;
      }
    }

    public bool highdefinition
    {
      get
      {
        return this._highdefinition;
      }
      set
      {
        this._highdefinition = value;
      }
    }

    public bool automatictranslation
    {
      get
      {
        return this._automatictranslation;
      }
      set
      {
        this._automatictranslation = value;
      }
    }

    public List<UploadSubtitleParameters> CDS
    {
      get
      {
        return this.cds;
      }
      set
      {
        this.cds = value;
      }
    }
  }
}
