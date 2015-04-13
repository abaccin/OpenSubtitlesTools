using System.Collections.Generic;

namespace XmlRpcHandler
{
  public struct XmlRpcMethodCall
  {
    private string name;
    private List<IXmlRpcValue> parameters;

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

    public List<IXmlRpcValue> Parameters
    {
      get
      {
        return this.parameters;
      }
      set
      {
        this.parameters = value;
      }
    }

    public XmlRpcMethodCall(string name)
    {
      this.name = name;
      this.parameters = new List<IXmlRpcValue>();
    }

    public XmlRpcMethodCall(string name, List<IXmlRpcValue> parameters)
    {
      this.name = name;
      this.parameters = parameters;
    }
  }
}
