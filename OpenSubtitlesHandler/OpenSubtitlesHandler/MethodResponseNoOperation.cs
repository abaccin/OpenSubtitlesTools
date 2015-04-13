namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("NoOperation method response", "NoOperation method response hold all expected values from server.")]
  public class MethodResponseNoOperation : IMethodResponse
  {
    private string _global_wrh_download_limit;
    private string _client_ip;
    private string _limit_check_by;
    private string _client_24h_download_count;
    private string _client_downlaod_quota;
    private string _client_24h_download_limit;

    public string global_wrh_download_limit
    {
      get
      {
        return this._global_wrh_download_limit;
      }
      set
      {
        this._global_wrh_download_limit = value;
      }
    }

    public string client_ip
    {
      get
      {
        return this._client_ip;
      }
      set
      {
        this._client_ip = value;
      }
    }

    public string limit_check_by
    {
      get
      {
        return this._limit_check_by;
      }
      set
      {
        this._limit_check_by = value;
      }
    }

    public string client_24h_download_count
    {
      get
      {
        return this._client_24h_download_count;
      }
      set
      {
        this._client_24h_download_count = value;
      }
    }

    public string client_downlaod_quota
    {
      get
      {
        return this._client_downlaod_quota;
      }
      set
      {
        this._client_downlaod_quota = value;
      }
    }

    public string client_24h_download_limit
    {
      get
      {
        return this._client_24h_download_limit;
      }
      set
      {
        this._client_24h_download_limit = value;
      }
    }

    public MethodResponseNoOperation()
    {
    }

    public MethodResponseNoOperation(string name, string message)
      : base(name, message)
    {
    }
  }
}
