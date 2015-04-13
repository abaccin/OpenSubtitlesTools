using System;

namespace XmlRpcHandler
{
  public class XmlRpcValueBasic : IXmlRpcValue
  {
    private XmlRpcBasicValueType type;

    public XmlRpcBasicValueType ValueType
    {
      get
      {
        return this.type;
      }
      set
      {
        this.type = value;
      }
    }

    public XmlRpcValueBasic()
    {
    }

    public XmlRpcValueBasic(string data)
      : base((object) data)
    {
      this.type = XmlRpcBasicValueType.String;
    }

    public XmlRpcValueBasic(int data)
      : base((object) data)
    {
      this.type = XmlRpcBasicValueType.Int;
    }

    public XmlRpcValueBasic(double data)
      : base((object) data)
    {
      this.type = XmlRpcBasicValueType.Double;
    }

    public XmlRpcValueBasic(DateTime data)
      : base((object) data)
    {
      this.type = XmlRpcBasicValueType.dateTime_iso8601;
    }

    public XmlRpcValueBasic(bool data)
      : base(data)
    {
      this.type = XmlRpcBasicValueType.Boolean;
    }

    public XmlRpcValueBasic(long data)
      : base((object) data)
    {
      this.type = XmlRpcBasicValueType.base64;
    }

    public XmlRpcValueBasic(object data, XmlRpcBasicValueType type)
      : base(data)
    {
      this.type = type;
    }

    public static implicit operator string(XmlRpcValueBasic f)
    {
      if (f.type == XmlRpcBasicValueType.String)
        return f.Data.ToString();
      else
        throw new Exception("Unable to convert, this value is not string type.");
    }

    public static implicit operator int(XmlRpcValueBasic f)
    {
      if (f.type == XmlRpcBasicValueType.String)
        return (int) f.Data;
      else
        throw new Exception("Unable to convert, this value is not int type.");
    }

    public static implicit operator double(XmlRpcValueBasic f)
    {
      if (f.type == XmlRpcBasicValueType.String)
        return (double) f.Data;
      else
        throw new Exception("Unable to convert, this value is not double type.");
    }

    public static implicit operator bool(XmlRpcValueBasic f)
    {
      if (f.type == XmlRpcBasicValueType.String)
        return (bool) f.Data;
      else
        throw new Exception("Unable to convert, this value is not bool type.");
    }

    public static implicit operator long(XmlRpcValueBasic f)
    {
      if (f.type == XmlRpcBasicValueType.String)
        return (long) f.Data;
      else
        throw new Exception("Unable to convert, this value is not long type.");
    }

    public static implicit operator DateTime(XmlRpcValueBasic f)
    {
      if (f.type == XmlRpcBasicValueType.String)
        return (DateTime) f.Data;
      else
        throw new Exception("Unable to convert, this value is not DateTime type.");
    }
  }
}
