namespace XmlRpcHandler
{
  public class XmlRpcStructMember
  {
    private string name;
    private IXmlRpcValue data;

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

    public IXmlRpcValue Data
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

    public XmlRpcStructMember(string name, IXmlRpcValue data)
    {
      this.name = name;
      this.data = data;
    }
  }
}
