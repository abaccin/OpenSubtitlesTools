using System;
using System.Collections.Generic;

namespace XmlRpcHandler
{
  public class XmlRpcValueArray : IXmlRpcValue
  {
    private List<IXmlRpcValue> values;

    public List<IXmlRpcValue> Values
    {
      get
      {
        return this.values;
      }
      set
      {
        this.values = value;
      }
    }

    public XmlRpcValueArray()
    {
      this.values = new List<IXmlRpcValue>();
    }

    public XmlRpcValueArray(object data)
      : base(data)
    {
      this.values = new List<IXmlRpcValue>();
    }

    public XmlRpcValueArray(string[] texts)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (string data in texts)
        this.values.Add((IXmlRpcValue) new XmlRpcValueBasic(data));
    }

    public XmlRpcValueArray(int[] ints)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (int data in ints)
        this.values.Add((IXmlRpcValue) new XmlRpcValueBasic(data));
    }

    public XmlRpcValueArray(double[] doubles)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (double data in doubles)
        this.values.Add((IXmlRpcValue) new XmlRpcValueBasic(data));
    }

    public XmlRpcValueArray(bool[] bools)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (bool data in bools)
        this.values.Add((IXmlRpcValue) new XmlRpcValueBasic(data));
    }

    public XmlRpcValueArray(long[] base24s)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (long data in base24s)
        this.values.Add((IXmlRpcValue) new XmlRpcValueBasic(data));
    }

    public XmlRpcValueArray(DateTime[] dates)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (DateTime data in dates)
        this.values.Add((IXmlRpcValue) new XmlRpcValueBasic(data));
    }

    public XmlRpcValueArray(XmlRpcValueBasic[] basicValues)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (IXmlRpcValue ixmlRpcValue in basicValues)
        this.values.Add(ixmlRpcValue);
    }

    public XmlRpcValueArray(XmlRpcValueStruct[] structs)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (IXmlRpcValue ixmlRpcValue in structs)
        this.values.Add(ixmlRpcValue);
    }

    public XmlRpcValueArray(XmlRpcValueArray[] arrays)
    {
      this.values = new List<IXmlRpcValue>();
      foreach (IXmlRpcValue ixmlRpcValue in arrays)
        this.values.Add(ixmlRpcValue);
    }
  }
}
