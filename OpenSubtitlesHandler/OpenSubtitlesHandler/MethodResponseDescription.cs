using System;

namespace OpenSubtitlesHandler
{
  public class MethodResponseDescription : Attribute
  {
    private string name;
    private string message;

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

    public string Message
    {
      get
      {
        return this.message;
      }
      set
      {
        this.message = value;
      }
    }

    public MethodResponseDescription(string name, string message)
    {
      this.name = name;
      this.message = message;
    }
  }
}
