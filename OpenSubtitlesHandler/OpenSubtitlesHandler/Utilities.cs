using GetHash;
using Ionic.Zlib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace OpenSubtitlesHandler
{
  public sealed class Utilities
  {
    private const string XML_RPC_SERVER = "http://api.opensubtitles.org/xml-rpc";

    public static string ComputeHash(string fileName)
    {
      return Main.ToHexadecimal(Main.ComputeHash(fileName));
    }

    public static string ComputeMd5(string filename)
    {
      MD5 md5 = MD5.Create();
      StringBuilder stringBuilder = new StringBuilder();
      Stream inputStream = (Stream) new FileStream(filename, FileMode.Open, FileAccess.Read);
      foreach (byte num in md5.ComputeHash(inputStream))
        stringBuilder.Append(num.ToString("x2").ToLower());
      inputStream.Close();
      return ((object) stringBuilder).ToString();
    }

    public static byte[] Decompress(Stream dataToDecompress)
    {
      MemoryStream memoryStream = new MemoryStream();
      using (System.IO.Compression.GZipStream gzipStream = new System.IO.Compression.GZipStream(dataToDecompress, System.IO.Compression.CompressionMode.Decompress))
        gzipStream.CopyTo((Stream) memoryStream);
      return memoryStream.GetBuffer();
    }

    public static byte[] Compress(Stream dataToCompress)
    {
      using (MemoryStream memoryStream = new MemoryStream())
      {
        using (ZlibStream zlibStream = new ZlibStream((Stream) memoryStream, Ionic.Zlib.CompressionMode.Compress, Ionic.Zlib.CompressionLevel.Default, false))
        {
          byte[] buffer = new byte[4096];
          int count;
          do
          {
            count = dataToCompress.Read(buffer, 0, buffer.Length);
            if (count > 0)
              zlibStream.Write(buffer, 0, count);
          }
          while (count > 0);
        }
        return memoryStream.ToArray();
      }
    }

    public static string GetStreamString(Stream responseStream, Encoding encoding)
    {
      List<byte> list = new List<byte>();
      while (true)
      {
        int num = responseStream.ReadByte();
        if (num >= 0)
          list.Add((byte) num);
        else
          break;
      }
      responseStream.Close();
      return encoding.GetString(list.ToArray());
    }

    public static string GetStreamString(Stream responseStream)
    {
      return Utilities.GetStreamString(responseStream, Encoding.ASCII);
    }

    public static Stream SendRequest(byte[] request, string userAgent)
    {
      HttpWebRequest httpWebRequest = (HttpWebRequest) WebRequest.Create("http://api.opensubtitles.org/xml-rpc");
      httpWebRequest.ContentType = "text/xml";
      httpWebRequest.Host = "api.opensubtitles.org:80";
      httpWebRequest.Method = "POST";
      httpWebRequest.UserAgent = "xmlrpc-epi-php/0.2 (PHP)";
      httpWebRequest.ContentLength = (long) request.Length;
      ServicePointManager.Expect100Continue = false;
      try
      {
        using (Stream requestStream = ((WebRequest) httpWebRequest).GetRequestStream())
          requestStream.Write(request, 0, request.Length);
        return httpWebRequest.GetResponse().GetResponseStream();
      }
      catch (Exception ex)
      {
        Stream stream = (Stream) new MemoryStream();
        byte[] bytes = Encoding.ASCII.GetBytes("ERROR: " + ex.Message);
        stream.Write(bytes, 0, bytes.Length);
        stream.Position = 0L;
        return stream;
      }
    }
  }
}
