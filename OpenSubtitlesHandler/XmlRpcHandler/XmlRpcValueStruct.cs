using System.Collections.Generic;

namespace XmlRpcHandler
{
  public class XmlRpcValueStruct : IXmlRpcValue
  {
    private List<XmlRpcStructMember> members;

    public List<XmlRpcStructMember> Members
    {
      get
      {
        return this.members;
      }
      set
      {
        this.members = value;
      }
    }

    public XmlRpcValueStruct(List<XmlRpcStructMember> members)
    {
      this.members = members;
    }
  }
}
