using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace XmlRpcHandler
{
  public class XmlRpcGenerator
  {
    public static byte[] Generate(XmlRpcMethodCall method)
    {
      return XmlRpcGenerator.Generate(new XmlRpcMethodCall[1]
      {
        method
      });
    }

    public static byte[] Generate(XmlRpcMethodCall[] methods)
    {
      if (methods == null)
        throw new Exception("No method to write !");
      if (methods.Length == 0)
        throw new Exception("No method to write !");
      XmlWriterSettings settings = new XmlWriterSettings();
      settings.Indent = true;
      settings.Encoding = Encoding.UTF8;
      Stream fileStream = new MemoryStream();
      XmlWriter XMLwrt = XmlWriter.Create((Stream)fileStream, settings);
      foreach (XmlRpcMethodCall xmlRpcMethodCall in methods)
      {
        XMLwrt.WriteStartElement("methodCall");
        XMLwrt.WriteStartElement("methodName");
        XMLwrt.WriteString(xmlRpcMethodCall.Name);
        XMLwrt.WriteEndElement();
        XMLwrt.WriteStartElement("params");
        foreach (IXmlRpcValue ixmlRpcValue in xmlRpcMethodCall.Parameters)
        {
          XMLwrt.WriteStartElement("param");
          if (ixmlRpcValue is XmlRpcValueBasic)
            XmlRpcGenerator.WriteBasicValue(XMLwrt, (XmlRpcValueBasic)ixmlRpcValue);
          else if (ixmlRpcValue is XmlRpcValueStruct)
            XmlRpcGenerator.WriteStructValue(XMLwrt, (XmlRpcValueStruct)ixmlRpcValue);
          else if (ixmlRpcValue is XmlRpcValueArray)
            XmlRpcGenerator.WriteArrayValue(XMLwrt, (XmlRpcValueArray)ixmlRpcValue);
          XMLwrt.WriteEndElement();
        }
        XMLwrt.WriteEndElement();
        XMLwrt.WriteEndElement();
      }
      XMLwrt.Flush();
      XMLwrt.Close();
      fileStream.Position = 0;
      var str = new StreamReader(fileStream).ReadToEnd();
      fileStream.Close();

      return Encoding.UTF8.GetBytes(str);
    }

    public static XmlRpcMethodCall[] DecodeMethodResponse(string xmlResponse)
    {
      List<XmlRpcMethodCall> list = new List<XmlRpcMethodCall>();
      XmlReaderSettings settings = new XmlReaderSettings();
      settings.DtdProcessing = DtdProcessing.Ignore;
      settings.IgnoreWhitespace = true;
      MemoryStream memoryStream = new MemoryStream(Encoding.ASCII.GetBytes(xmlResponse));
      if (xmlResponse.Contains("encoding=\"utf-8\""))
        memoryStream = new MemoryStream(Encoding.UTF8.GetBytes(xmlResponse));
      XmlReader xmlReader = XmlReader.Create((Stream)memoryStream, settings);
      XmlRpcMethodCall xmlRpcMethodCall = new XmlRpcMethodCall("methodResponse");
      while (xmlReader.Read())
      {
        if (xmlReader.Name == "param" && xmlReader.IsStartElement())
        {
          IXmlRpcValue ixmlRpcValue = XmlRpcGenerator.ReadValue(xmlReader);
          if (ixmlRpcValue != null)
            xmlRpcMethodCall.Parameters.Add(ixmlRpcValue);
        }
      }
      list.Add(xmlRpcMethodCall);
      xmlReader.Close();
      return list.ToArray();
    }

    private static void WriteBasicValue(XmlWriter XMLwrt, XmlRpcValueBasic val)
    {
      XMLwrt.WriteStartElement("value");
      switch (val.ValueType)
      {
        case XmlRpcBasicValueType.String:
          XMLwrt.WriteStartElement("string");
          if (val.Data != null)
            XMLwrt.WriteString(val.Data.ToString());
          XMLwrt.WriteEndElement();
          break;
        case XmlRpcBasicValueType.Int:
          XMLwrt.WriteStartElement("int");
          if (val.Data != null)
            XMLwrt.WriteString(val.Data.ToString());
          XMLwrt.WriteEndElement();
          break;
        case XmlRpcBasicValueType.Boolean:
          XMLwrt.WriteStartElement("boolean");
          if (val.Data != null)
            XMLwrt.WriteString((bool)val.Data ? "1" : "0");
          XMLwrt.WriteEndElement();
          break;
        case XmlRpcBasicValueType.Double:
          XMLwrt.WriteStartElement("double");
          if (val.Data != null)
            XMLwrt.WriteString(val.Data.ToString());
          XMLwrt.WriteEndElement();
          break;
        case XmlRpcBasicValueType.dateTime_iso8601:
          XMLwrt.WriteStartElement("dateTime.iso8601");
          if (val.Data != null)
          {
            DateTime dateTime = (DateTime)val.Data;
            string text = (string)(object)dateTime.Year + (object)dateTime.Month.ToString("D2") + dateTime.Day.ToString("D2") + "T" + dateTime.Hour.ToString("D2") + ":" + dateTime.Minute.ToString("D2") + ":" + dateTime.Second.ToString("D2");
            XMLwrt.WriteString(text);
          }
          XMLwrt.WriteEndElement();
          break;
        case XmlRpcBasicValueType.base64:
          XMLwrt.WriteStartElement("base64");
          if (val.Data != null)
            XMLwrt.WriteString(Convert.ToBase64String(BitConverter.GetBytes((long)val.Data)));
          XMLwrt.WriteEndElement();
          break;
      }
      XMLwrt.WriteEndElement();
    }

    private static void WriteStructValue(XmlWriter XMLwrt, XmlRpcValueStruct val)
    {
      XMLwrt.WriteStartElement("value");
      XMLwrt.WriteStartElement("struct");
      foreach (XmlRpcStructMember xmlRpcStructMember in val.Members)
      {
        XMLwrt.WriteStartElement("member");
        XMLwrt.WriteStartElement("name");
        XMLwrt.WriteString(xmlRpcStructMember.Name);
        XMLwrt.WriteEndElement();
        if (xmlRpcStructMember.Data is XmlRpcValueBasic)
          XmlRpcGenerator.WriteBasicValue(XMLwrt, (XmlRpcValueBasic)xmlRpcStructMember.Data);
        else if (xmlRpcStructMember.Data is XmlRpcValueStruct)
          XmlRpcGenerator.WriteStructValue(XMLwrt, (XmlRpcValueStruct)xmlRpcStructMember.Data);
        else if (xmlRpcStructMember.Data is XmlRpcValueArray)
          XmlRpcGenerator.WriteArrayValue(XMLwrt, (XmlRpcValueArray)xmlRpcStructMember.Data);
        XMLwrt.WriteEndElement();
      }
      XMLwrt.WriteEndElement();
      XMLwrt.WriteEndElement();
    }

    private static void WriteArrayValue(XmlWriter XMLwrt, XmlRpcValueArray val)
    {
      XMLwrt.WriteStartElement("value");
      XMLwrt.WriteStartElement("array");
      XMLwrt.WriteStartElement("data");
      foreach (IXmlRpcValue ixmlRpcValue in val.Values)
      {
        if (ixmlRpcValue is XmlRpcValueBasic)
          XmlRpcGenerator.WriteBasicValue(XMLwrt, (XmlRpcValueBasic)ixmlRpcValue);
        else if (ixmlRpcValue is XmlRpcValueStruct)
          XmlRpcGenerator.WriteStructValue(XMLwrt, (XmlRpcValueStruct)ixmlRpcValue);
        else if (ixmlRpcValue is XmlRpcValueArray)
          XmlRpcGenerator.WriteArrayValue(XMLwrt, (XmlRpcValueArray)ixmlRpcValue);
      }
      XMLwrt.WriteEndElement();
      XMLwrt.WriteEndElement();
      XMLwrt.WriteEndElement();
    }

    private static IXmlRpcValue ReadValue(XmlReader xmlReader)
    {
      while (xmlReader.Read() && (xmlReader.Name == "value" && xmlReader.IsStartElement()))
      {
        xmlReader.Read();
        if (xmlReader.Name == "string" && xmlReader.IsStartElement())
          return (IXmlRpcValue)new XmlRpcValueBasic((object)xmlReader.ReadString(), XmlRpcBasicValueType.String);
        if (xmlReader.Name == "int" && xmlReader.IsStartElement())
          return (IXmlRpcValue)new XmlRpcValueBasic((object)int.Parse(xmlReader.ReadString()), XmlRpcBasicValueType.Int);
        if (xmlReader.Name == "boolean" && xmlReader.IsStartElement())
        {
          return new XmlRpcValueBasic(xmlReader.ReadString() == "1", XmlRpcBasicValueType.Boolean);
        }
        if (xmlReader.Name == "double" && xmlReader.IsStartElement())
          return (IXmlRpcValue)new XmlRpcValueBasic((object)double.Parse(xmlReader.ReadString()), XmlRpcBasicValueType.Double);
        if (xmlReader.Name == "dateTime.iso8601" && xmlReader.IsStartElement())
        {
          string str = xmlReader.ReadString();
          return (IXmlRpcValue)new XmlRpcValueBasic((object)new DateTime(int.Parse(str.Substring(0, 4)), int.Parse(str.Substring(4, 2)), int.Parse(str.Substring(6, 2)), int.Parse(str.Substring(9, 2)), int.Parse(str.Substring(12, 2)), int.Parse(str.Substring(15, 2))), XmlRpcBasicValueType.dateTime_iso8601);
        }
        else
        {
          if (xmlReader.Name == "base64" && xmlReader.IsStartElement())
            return (IXmlRpcValue)new XmlRpcValueBasic((object)BitConverter.ToInt64(Convert.FromBase64String(xmlReader.ReadString()), 0), XmlRpcBasicValueType.Double);
          if (xmlReader.Name == "struct" && xmlReader.IsStartElement())
          {
            XmlRpcValueStruct xmlRpcValueStruct = new XmlRpcValueStruct(new List<XmlRpcStructMember>());
            while (xmlReader.Read())
            {
              if (xmlReader.Name == "member" && xmlReader.IsStartElement())
              {
                XmlRpcStructMember xmlRpcStructMember = new XmlRpcStructMember("", (IXmlRpcValue)null);
                xmlReader.Read();
                xmlRpcStructMember.Name = xmlReader.ReadString();
                IXmlRpcValue ixmlRpcValue = XmlRpcGenerator.ReadValue(xmlReader);
                if (ixmlRpcValue != null)
                {
                  xmlRpcStructMember.Data = ixmlRpcValue;
                  xmlRpcValueStruct.Members.Add(xmlRpcStructMember);
                }
              }
              else if (xmlReader.Name == "struct" && !xmlReader.IsStartElement())
                return (IXmlRpcValue)xmlRpcValueStruct;
            }
            return (IXmlRpcValue)xmlRpcValueStruct;
          }
          else if (xmlReader.Name == "array" && xmlReader.IsStartElement())
          {
            XmlRpcValueArray xmlRpcValueArray = new XmlRpcValueArray();
            while (xmlReader.Read())
            {
              if (xmlReader.Name == "array" && !xmlReader.IsStartElement())
                return (IXmlRpcValue)xmlRpcValueArray;
              IXmlRpcValue ixmlRpcValue = XmlRpcGenerator.ReadValue(xmlReader);
              if (ixmlRpcValue != null)
                xmlRpcValueArray.Values.Add(ixmlRpcValue);
            }
            return (IXmlRpcValue)xmlRpcValueArray;
          }
        }
      }
      return (IXmlRpcValue)null;
    }
  }
}
