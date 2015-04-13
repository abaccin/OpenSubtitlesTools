using System.Collections.Generic;

namespace OpenSubtitlesHandler
{
  [MethodResponseDescription("MovieDetails method response", "MovieDetails method response hold all expected values from server.")]
  public class MethodResponseMovieDetails : IMethodResponse
  {
    private List<string> cast = new List<string>();
    private List<string> directors = new List<string>();
    private List<string> writers = new List<string>();
    private List<string> awards = new List<string>();
    private List<string> genres = new List<string>();
    private List<string> country = new List<string>();
    private List<string> language = new List<string>();
    private List<string> certification = new List<string>();
    private string id;
    private string title;
    private string year;
    private string coverLink;
    private string duration;
    private string tagline;
    private string plot;
    private string goofs;
    private string trivia;

    public string ID
    {
      get
      {
        return this.id;
      }
      set
      {
        this.id = value;
      }
    }

    public string Title
    {
      get
      {
        return this.title;
      }
      set
      {
        this.title = value;
      }
    }

    public string Year
    {
      get
      {
        return this.year;
      }
      set
      {
        this.year = value;
      }
    }

    public string CoverLink
    {
      get
      {
        return this.coverLink;
      }
      set
      {
        this.coverLink = value;
      }
    }

    public string Duration
    {
      get
      {
        return this.duration;
      }
      set
      {
        this.duration = value;
      }
    }

    public string Tagline
    {
      get
      {
        return this.tagline;
      }
      set
      {
        this.tagline = value;
      }
    }

    public string Plot
    {
      get
      {
        return this.plot;
      }
      set
      {
        this.plot = value;
      }
    }

    public string Goofs
    {
      get
      {
        return this.goofs;
      }
      set
      {
        this.goofs = value;
      }
    }

    public string Trivia
    {
      get
      {
        return this.trivia;
      }
      set
      {
        this.trivia = value;
      }
    }

    public List<string> Cast
    {
      get
      {
        return this.cast;
      }
      set
      {
        this.cast = value;
      }
    }

    public List<string> Directors
    {
      get
      {
        return this.directors;
      }
      set
      {
        this.directors = value;
      }
    }

    public List<string> Writers
    {
      get
      {
        return this.writers;
      }
      set
      {
        this.writers = value;
      }
    }

    public List<string> Genres
    {
      get
      {
        return this.genres;
      }
      set
      {
        this.genres = value;
      }
    }

    public List<string> Awards
    {
      get
      {
        return this.awards;
      }
      set
      {
        this.awards = value;
      }
    }

    public List<string> Country
    {
      get
      {
        return this.country;
      }
      set
      {
        this.country = value;
      }
    }

    public List<string> Language
    {
      get
      {
        return this.language;
      }
      set
      {
        this.language = value;
      }
    }

    public List<string> Certification
    {
      get
      {
        return this.certification;
      }
      set
      {
        this.certification = value;
      }
    }

    public MethodResponseMovieDetails()
    {
    }

    public MethodResponseMovieDetails(string name, string message)
      : base(name, message)
    {
    }
  }
}
