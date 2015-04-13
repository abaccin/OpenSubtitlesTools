namespace XmlRpcHandler
{
  public abstract class IXmlRpcValue
  {
    private object data;

    public virtual object Data
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

    public IXmlRpcValue()
    {
    }

    public IXmlRpcValue(object data)
    {
      this.data = data;
    }
  }
}
