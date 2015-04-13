using System;
using System.ComponentModel;
using System.Reflection;

namespace OpenSubtitlesHandler
{
  public abstract class IMethodResponse
  {
    protected string name;
    protected string message;
    protected double seconds;
    protected string status;

    [Category("MethodResponse")]
    [Description("The name of this response")]
    public virtual string Name
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

    [Category("MethodResponse")]
    [Description("The message about this response")]
    public virtual string Message
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

    [Description("Time taken to execute this command on server")]
    [Category("MethodResponse")]
    public double Seconds
    {
      get
      {
        return this.seconds;
      }
      set
      {
        this.seconds = value;
      }
    }

    [Description("The status")]
    [Category("MethodResponse")]
    public string Status
    {
      get
      {
        return this.status;
      }
      set
      {
        this.status = value;
      }
    }

    public IMethodResponse()
    {
      this.LoadAttributes();
    }

    public IMethodResponse(string name, string message)
    {
      this.name = name;
      this.message = message;
    }

    protected virtual void LoadAttributes()
    {
      foreach (Attribute attribute in Attribute.GetCustomAttributes((MemberInfo) this.GetType()))
      {
        if (attribute.GetType() == typeof (MethodResponseDescription))
        {
          this.name = ((MethodResponseDescription) attribute).Name;
          this.message = ((MethodResponseDescription) attribute).Message;
          break;
        }
      }
    }
  }
}
