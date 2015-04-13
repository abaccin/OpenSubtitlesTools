using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  public class CheckMovieHash2Result
  {
    private List<CheckMovieHash2Data> data = new List<CheckMovieHash2Data>();
    private string name;

    public string Name
    {
      get
      {
        return this.name;
      }
      set
      {
        this.name = value;
      }
    }

    public List<CheckMovieHash2Data> Items
    {
      get
      {
        return this.data;
      }
      set
      {
        this.data = value;
      }
    }

    public override string ToString()
    {
      return this.name;
    }
  }
}
