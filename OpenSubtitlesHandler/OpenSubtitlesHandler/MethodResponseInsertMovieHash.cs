using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("InsertMovieHash method response", "InsertMovieHash method response hold all expected values from server.")]
  public class MethodResponseInsertMovieHash : IMethodResponse
  {
    private List<string> _accepted_moviehashes = new List<string>();
    private List<string> _new_imdbs = new List<string>();

    public List<string> accepted_moviehashes
    {
      get
      {
        return this._accepted_moviehashes;
      }
      set
      {
        this._accepted_moviehashes = value;
      }
    }

    public List<string> new_imdbs
    {
      get
      {
        return this._new_imdbs;
      }
      set
      {
        this._new_imdbs = value;
      }
    }

    public MethodResponseInsertMovieHash()
    {
    }

    public MethodResponseInsertMovieHash(string name, string message)
      : base(name, message)
    {
    }
  }
}
