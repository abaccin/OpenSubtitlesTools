using System.Collections.Generic;
using System.ComponentModel;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("ServerInfo method response", "ServerInfo method response hold all expected values from server.")]
  public class MethodResponseServerInfo : IMethodResponse
  {
    private List<string> _last_update_strings = new List<string>();
    private string _xmlrpc_version;
    private string _xmlrpc_url;
    private string _application;
    private string _contact;
    private string _website_url;
    private int _users_online_total;
    private int _users_online_program;
    private int _users_loggedin;
    private string _users_max_alltime;
    private string _users_registered;
    private string _subs_downloads;
    private string _subs_subtitle_files;
    private string _movies_total;
    private string _movies_aka;
    private string _total_subtitles_languages;

    [Description("Version of server's XML-RPC API implementation")]
    [Category("OS")]
    public string xmlrpc_version
    {
      get
      {
        return this._xmlrpc_version;
      }
      set
      {
        this._xmlrpc_version = value;
      }
    }

    [Description("XML-RPC interface URL")]
    [Category("OS")]
    public string xmlrpc_url
    {
      get
      {
        return this._xmlrpc_url;
      }
      set
      {
        this._xmlrpc_url = value;
      }
    }

    [Category("OS")]
    [Description("Server's application name and version")]
    public string application
    {
      get
      {
        return this._application;
      }
      set
      {
        this._application = value;
      }
    }

    [Description("Contact e-mail address for server related quuestions and problems")]
    [Category("OS")]
    public string contact
    {
      get
      {
        return this._contact;
      }
      set
      {
        this._contact = value;
      }
    }

    [Category("OS")]
    [Description("Main server URL")]
    public string website_url
    {
      get
      {
        return this._website_url;
      }
      set
      {
        this._website_url = value;
      }
    }

    [Category("OS")]
    [Description("Number of users currently online")]
    public int users_online_total
    {
      get
      {
        return this._users_online_total;
      }
      set
      {
        this._users_online_total = value;
      }
    }

    [Category("OS")]
    [Description("Number of users currently online using a client application (XML-RPC API)")]
    public int users_online_program
    {
      get
      {
        return this._users_online_program;
      }
      set
      {
        this._users_online_program = value;
      }
    }

    [Description("Number of currently logged-in users")]
    [Category("OS")]
    public int users_loggedin
    {
      get
      {
        return this._users_loggedin;
      }
      set
      {
        this._users_loggedin = value;
      }
    }

    [Description("Maximum number of users throughout the history")]
    [Category("OS")]
    public string users_max_alltime
    {
      get
      {
        return this._users_max_alltime;
      }
      set
      {
        this._users_max_alltime = value;
      }
    }

    [Description("Number of registered users")]
    [Category("OS")]
    public string users_registered
    {
      get
      {
        return this._users_registered;
      }
      set
      {
        this._users_registered = value;
      }
    }

    [Category("OS")]
    [Description("Total number of subtitle downloads")]
    public string subs_downloads
    {
      get
      {
        return this._subs_downloads;
      }
      set
      {
        this._subs_downloads = value;
      }
    }

    [Description("Total number of subtitle files stored on the server")]
    [Category("OS")]
    public string subs_subtitle_files
    {
      get
      {
        return this._subs_subtitle_files;
      }
      set
      {
        this._subs_subtitle_files = value;
      }
    }

    [Description("Total number of movies in the database")]
    [Category("OS")]
    public string movies_total
    {
      get
      {
        return this._movies_total;
      }
      set
      {
        this._movies_total = value;
      }
    }

    [Description("Total number of movie A.K.A. titles in the database")]
    [Category("OS")]
    public string movies_aka
    {
      get
      {
        return this._movies_aka;
      }
      set
      {
        this._movies_aka = value;
      }
    }

    [Category("OS")]
    [Description("Total number of subtitle languages supported")]
    public string total_subtitles_languages
    {
      get
      {
        return this._total_subtitles_languages;
      }
      set
      {
        this._total_subtitles_languages = value;
      }
    }

    [Description("Structure containing information about last updates of translations")]
    [Category("OS")]
    public List<string> last_update_strings
    {
      get
      {
        return this._last_update_strings;
      }
      set
      {
        this._last_update_strings = value;
      }
    }

    public MethodResponseServerInfo()
    {
    }

    public MethodResponseServerInfo(string name, string message)
      : base(name, message)
    {
    }
  }
}
