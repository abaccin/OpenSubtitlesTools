using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using XmlRpcHandler;

namespace OpenSubtitlesHandler
{
  public sealed class OpenSubtitles
  {
    private static string XML_PRC_USERAGENT = "";
    private static string TOKEN = "";

    public static void SetUserAgent(string agent)
    {
      OpenSubtitles.XML_PRC_USERAGENT = agent;
    }

    public static IMethodResponse LogIn(string userName, string password, string language)
    {
      XmlRpcMethodCall method = new XmlRpcMethodCall("LogIn", new List<IXmlRpcValue>()
      {
        (IXmlRpcValue) new XmlRpcValueBasic(userName),
        (IXmlRpcValue) new XmlRpcValueBasic(password),
        (IXmlRpcValue) new XmlRpcValueBasic(language),
        (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.XML_PRC_USERAGENT)
      });
      OSHConsole.WriteLine("Sending LogIn request to the server ...", DebugCode.Good);
      string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
      if (!streamString.Contains("ERROR:"))
      {
        XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
        if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
          return (IMethodResponse) new MethodResponseError("Fail", "Log in failed !");
        XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
        MethodResponseLogIn methodResponseLogIn = new MethodResponseLogIn("Success", "Log in successful.");
        foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
        {
          switch (xmlRpcStructMember.Name)
          {
            case "token":
              methodResponseLogIn.Token = OpenSubtitles.TOKEN = xmlRpcStructMember.Data.Data.ToString();
              OSHConsole.WriteLine(xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            case "seconds":
              methodResponseLogIn.Seconds = (double) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine(xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            case "status":
              methodResponseLogIn.Status = xmlRpcStructMember.Data.Data.ToString();
              OSHConsole.WriteLine(xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            default:
              continue;
          }
        }
        return (IMethodResponse) methodResponseLogIn;
      }
      else
      {
        OSHConsole.WriteLine(streamString, DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", streamString);
      }
    }

    public static IMethodResponse LogOut()
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("LogOut", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String)
        });
        OSHConsole.WriteLine("Sending LogOut request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "Log out failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          OSHConsole.WriteLine("STATUS=" + xmlRpcValueStruct.Members[0].Data.Data.ToString(), DebugCode.None);
          OSHConsole.WriteLine("SECONDS=" + xmlRpcValueStruct.Members[1].Data.Data.ToString(), DebugCode.None);
          MethodResponseLogIn methodResponseLogIn = new MethodResponseLogIn("Success", "Log out successful.");
          methodResponseLogIn.Status = xmlRpcValueStruct.Members[0].Data.Data.ToString();
          methodResponseLogIn.Seconds = (double) xmlRpcValueStruct.Members[1].Data.Data;
          return (IMethodResponse) methodResponseLogIn;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse NoOperation()
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("NoOperation", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.XML_PRC_USERAGENT, XmlRpcBasicValueType.String)
        });
        OSHConsole.WriteLine("Sending NoOperation request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "NoOperation call failed !");
          MethodResponseNoOperation responseNoOperation = new MethodResponseNoOperation();
          foreach (XmlRpcStructMember xmlRpcStructMember in ((XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0]).Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseNoOperation.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(xmlRpcStructMember.Name + (object) "= " + (string) xmlRpcStructMember.Data.Data, DebugCode.None);
                continue;
              case "seconds":
                responseNoOperation.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(xmlRpcStructMember.Name + (object) "= " + (string) xmlRpcStructMember.Data.Data, DebugCode.None);
                continue;
              case "download_limits":
                using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember.Data).Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    OSHConsole.WriteLine(" >" + current.Name + "= " + current.Data.Data.ToString(), DebugCode.None);
                    switch (current.Name)
                    {
                      case "global_wrh_download_limit":
                        responseNoOperation.global_wrh_download_limit = current.Data.Data.ToString();
                        continue;
                      case "client_ip":
                        responseNoOperation.client_ip = current.Data.Data.ToString();
                        continue;
                      case "limit_check_by":
                        responseNoOperation.limit_check_by = current.Data.Data.ToString();
                        continue;
                      case "client_24h_download_count":
                        responseNoOperation.client_24h_download_count = current.Data.Data.ToString();
                        continue;
                      case "client_downlaod_quota":
                        responseNoOperation.client_downlaod_quota = current.Data.Data.ToString();
                        continue;
                      case "client_24h_download_limit":
                        responseNoOperation.client_24h_download_limit = current.Data.Data.ToString();
                        continue;
                      default:
                        continue;
                    }
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseNoOperation;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse SearchSubtitles(SubtitleSearchParameters[] parameters)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else if (parameters == null)
      {
        OSHConsole.UpdateLine("No subtitle search parameter passed !!", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "No subtitle search parameter passed");
      }
      else if (parameters.Length == 0)
      {
        OSHConsole.UpdateLine("No subtitle search parameter passed !!", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "No subtitle search parameter passed");
      }
      else
      {
        List<IXmlRpcValue> parameters1 = new List<IXmlRpcValue>();
        parameters1.Add((IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String));
        XmlRpcValueArray xmlRpcValueArray = new XmlRpcValueArray();
        foreach (SubtitleSearchParameters searchParameters in parameters)
        {
          XmlRpcValueStruct xmlRpcValueStruct = new XmlRpcValueStruct(new List<XmlRpcStructMember>());
          XmlRpcStructMember xmlRpcStructMember1 = new XmlRpcStructMember("sublanguageid", (IXmlRpcValue) new XmlRpcValueBasic((object) searchParameters.SubLangaugeID, XmlRpcBasicValueType.String));
          xmlRpcValueStruct.Members.Add(xmlRpcStructMember1);
          XmlRpcStructMember xmlRpcStructMember2 = new XmlRpcStructMember("moviehash", (IXmlRpcValue) new XmlRpcValueBasic((object) searchParameters.MovieHash, XmlRpcBasicValueType.String));
          xmlRpcValueStruct.Members.Add(xmlRpcStructMember2);
          XmlRpcStructMember xmlRpcStructMember3 = new XmlRpcStructMember("moviebytesize", (IXmlRpcValue) new XmlRpcValueBasic((object) searchParameters.MovieByteSize, XmlRpcBasicValueType.Int));
          xmlRpcValueStruct.Members.Add(xmlRpcStructMember3);
          if (searchParameters.IMDbID.Length > 0)
          {
            XmlRpcStructMember xmlRpcStructMember4 = new XmlRpcStructMember("imdbid", (IXmlRpcValue) new XmlRpcValueBasic((object) searchParameters.IMDbID, XmlRpcBasicValueType.String));
            xmlRpcValueStruct.Members.Add(xmlRpcStructMember4);
          }
          xmlRpcValueArray.Values.Add((IXmlRpcValue) xmlRpcValueStruct);
        }
        parameters1.Add((IXmlRpcValue) xmlRpcValueArray);
        XmlRpcMethodCall method = new XmlRpcMethodCall("SearchSubtitles", parameters1);
        OSHConsole.WriteLine("Sending SearchSubtitles request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "Search Subtitles call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseSubtitleSearch responseSubtitleSearch = new MethodResponseSubtitleSearch();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct.Members)
          {
            if (xmlRpcStructMember1.Name == "status")
            {
              responseSubtitleSearch.Status = (string) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Status= " + responseSubtitleSearch.Status, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "seconds")
            {
              responseSubtitleSearch.Seconds = (double) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Seconds= " + (object) responseSubtitleSearch.Seconds, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "data")
            {
              if (xmlRpcStructMember1.Data is XmlRpcValueArray)
              {
                OSHConsole.WriteLine("Search results: ", DebugCode.None);
                foreach (IXmlRpcValue ixmlRpcValue in ((XmlRpcValueArray) xmlRpcStructMember1.Data).Values)
                {
                  if (ixmlRpcValue != null && ixmlRpcValue is XmlRpcValueStruct)
                  {
                    SubtitleSearchResult subtitleSearchResult = new SubtitleSearchResult();
                    foreach (XmlRpcStructMember xmlRpcStructMember2 in ((XmlRpcValueStruct) ixmlRpcValue).Members)
                    {
                      switch (xmlRpcStructMember2.Name)
                      {
                        case "IDMovie":
                          subtitleSearchResult.IDMovie = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "IDMovieImdb":
                          subtitleSearchResult.IDMovieImdb = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "IDSubMovieFile":
                          subtitleSearchResult.IDSubMovieFile = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "IDSubtitle":
                          subtitleSearchResult.IDSubtitle = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "IDSubtitleFile":
                          subtitleSearchResult.IDSubtitleFile = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "ISO639":
                          subtitleSearchResult.ISO639 = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "LanguageName":
                          subtitleSearchResult.LanguageName = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieByteSize":
                          subtitleSearchResult.MovieByteSize = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieHash":
                          subtitleSearchResult.MovieHash = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieImdbRating":
                          subtitleSearchResult.MovieImdbRating = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieName":
                          subtitleSearchResult.MovieName = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieNameEng":
                          subtitleSearchResult.MovieNameEng = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieReleaseName":
                          subtitleSearchResult.MovieReleaseName = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieTimeMS":
                          subtitleSearchResult.MovieTimeMS = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "MovieYear":
                          subtitleSearchResult.MovieYear = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubActualCD":
                          subtitleSearchResult.SubActualCD = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubAddDate":
                          subtitleSearchResult.SubAddDate = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubAuthorComment":
                          subtitleSearchResult.SubAuthorComment = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubBad":
                          subtitleSearchResult.SubBad = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubDownloadLink":
                          subtitleSearchResult.SubDownloadLink = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubDownloadsCnt":
                          subtitleSearchResult.SubDownloadsCnt = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubFileName":
                          subtitleSearchResult.SubFileName = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubFormat":
                          subtitleSearchResult.SubFormat = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubHash":
                          subtitleSearchResult.SubHash = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubLanguageID":
                          subtitleSearchResult.SubLanguageID = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubRating":
                          subtitleSearchResult.SubRating = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubSize":
                          subtitleSearchResult.SubSize = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "SubSumCD":
                          subtitleSearchResult.SubSumCD = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "UserID":
                          subtitleSearchResult.UserID = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "UserNickName":
                          subtitleSearchResult.UserNickName = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        case "ZipDownloadLink":
                          subtitleSearchResult.ZipDownloadLink = xmlRpcStructMember2.Data.Data.ToString();
                          continue;
                        default:
                          continue;
                      }
                    }
                    responseSubtitleSearch.Results.Add(subtitleSearchResult);
                    OSHConsole.WriteLine(">" + subtitleSearchResult.ToString(), DebugCode.None);
                  }
                }
              }
              else
                OSHConsole.WriteLine("Data= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.Warning);
            }
          }
          return (IMethodResponse) responseSubtitleSearch;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse DownloadSubtitles(int[] subIDS)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else if (subIDS == null)
      {
        OSHConsole.UpdateLine("No subtitle id passed !!", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "No subtitle id passed");
      }
      else if (subIDS.Length == 0)
      {
        OSHConsole.UpdateLine("No subtitle id passed !!", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "No subtitle id passed");
      }
      else
      {
        List<IXmlRpcValue> parameters = new List<IXmlRpcValue>();
        parameters.Add((IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String));
        XmlRpcValueArray xmlRpcValueArray = new XmlRpcValueArray();
        foreach (int num in subIDS)
          xmlRpcValueArray.Values.Add((IXmlRpcValue) new XmlRpcValueBasic((object) num, XmlRpcBasicValueType.Int));
        parameters.Add((IXmlRpcValue) xmlRpcValueArray);
        XmlRpcMethodCall method = new XmlRpcMethodCall("DownloadSubtitles", parameters);
        OSHConsole.WriteLine("Sending DownloadSubtitles request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "DownloadSubtitles call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseSubtitleDownload subtitleDownload = new MethodResponseSubtitleDownload();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct.Members)
          {
            if (xmlRpcStructMember1.Name == "status")
            {
              subtitleDownload.Status = (string) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Status= " + subtitleDownload.Status, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "seconds")
            {
              subtitleDownload.Seconds = (double) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Seconds= " + (object) subtitleDownload.Seconds, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "data")
            {
              if (xmlRpcStructMember1.Data is XmlRpcValueArray)
              {
                OSHConsole.WriteLine("Download results:", DebugCode.None);
                foreach (IXmlRpcValue ixmlRpcValue in ((XmlRpcValueArray) xmlRpcStructMember1.Data).Values)
                {
                  if (ixmlRpcValue != null && ixmlRpcValue is XmlRpcValueStruct)
                  {
                    SubtitleDownloadResult subtitleDownloadResult = new SubtitleDownloadResult();
                    foreach (XmlRpcStructMember xmlRpcStructMember2 in ((XmlRpcValueStruct) ixmlRpcValue).Members)
                    {
                      switch (xmlRpcStructMember2.Name)
                      {
                        case "idsubtitlefile":
                          subtitleDownloadResult.IdSubtitleFile = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        case "data":
                          subtitleDownloadResult.Data = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        default:
                          continue;
                      }
                    }
                    subtitleDownload.Results.Add(subtitleDownloadResult);
                    OSHConsole.WriteLine("> IDSubtilteFile= " + subtitleDownloadResult.ToString(), DebugCode.None);
                  }
                }
              }
              else
                OSHConsole.WriteLine("Data= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.Warning);
            }
          }
          return (IMethodResponse) subtitleDownload;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse GetComments(int[] subIDS)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else if (subIDS == null)
      {
        OSHConsole.UpdateLine("No subtitle id passed !!", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "No subtitle id passed");
      }
      else if (subIDS.Length == 0)
      {
        OSHConsole.UpdateLine("No subtitle id passed !!", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "No subtitle id passed");
      }
      else
      {
        List<IXmlRpcValue> parameters = new List<IXmlRpcValue>();
        parameters.Add((IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN));
        XmlRpcValueArray xmlRpcValueArray = new XmlRpcValueArray(subIDS);
        parameters.Add((IXmlRpcValue) xmlRpcValueArray);
        XmlRpcMethodCall method = new XmlRpcMethodCall("GetComments", parameters);
        OSHConsole.WriteLine("Sending GetComments request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "GetComments call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseGetComments responseGetComments = new MethodResponseGetComments();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct.Members)
          {
            if (xmlRpcStructMember1.Name == "status")
            {
              responseGetComments.Status = (string) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Status= " + responseGetComments.Status, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "seconds")
            {
              responseGetComments.Seconds = (double) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Seconds= " + (object) responseGetComments.Seconds, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "data")
            {
              if (xmlRpcStructMember1.Data is XmlRpcValueArray)
              {
                OSHConsole.WriteLine("Comments results:", DebugCode.None);
                foreach (IXmlRpcValue ixmlRpcValue in ((XmlRpcValueArray) xmlRpcStructMember1.Data).Values)
                {
                  if (ixmlRpcValue != null && ixmlRpcValue is XmlRpcValueStruct)
                  {
                    GetCommentsResult getCommentsResult = new GetCommentsResult();
                    foreach (XmlRpcStructMember xmlRpcStructMember2 in ((XmlRpcValueStruct) ixmlRpcValue).Members)
                    {
                      switch (xmlRpcStructMember2.Name)
                      {
                        case "IDSubtitle":
                          getCommentsResult.IDSubtitle = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        case "UserID":
                          getCommentsResult.UserID = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        case "UserNickName":
                          getCommentsResult.UserNickName = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        case "Comment":
                          getCommentsResult.Comment = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        case "Created":
                          getCommentsResult.Created = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        default:
                          continue;
                      }
                    }
                    responseGetComments.Results.Add(getCommentsResult);
                    OSHConsole.WriteLine("> IDSubtitle= " + getCommentsResult.ToString(), DebugCode.None);
                  }
                }
              }
              else
                OSHConsole.WriteLine("Data= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.Warning);
            }
          }
          return (IMethodResponse) responseGetComments;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse SearchToMail(string[] languageIDS, SearchToMailMovieParameter[] movies)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        List<IXmlRpcValue> parameters = new List<IXmlRpcValue>();
        parameters.Add((IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String));
        XmlRpcValueArray xmlRpcValueArray1 = new XmlRpcValueArray(languageIDS);
        parameters.Add((IXmlRpcValue) xmlRpcValueArray1);
        XmlRpcValueArray xmlRpcValueArray2 = new XmlRpcValueArray();
        foreach (SearchToMailMovieParameter mailMovieParameter in movies)
          xmlRpcValueArray2.Values.Add((IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("moviehash", (IXmlRpcValue) new XmlRpcValueBasic(mailMovieParameter.moviehash)),
              new XmlRpcStructMember("moviesize", (IXmlRpcValue) new XmlRpcValueBasic(mailMovieParameter.moviesize))
            }
          });
        parameters.Add((IXmlRpcValue) xmlRpcValueArray2);
        XmlRpcMethodCall method = new XmlRpcMethodCall("SearchToMail", parameters);
        OSHConsole.WriteLine("Sending SearchToMail request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "SearchToMail call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseSearchToMail responseSearchToMail = new MethodResponseSearchToMail();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseSearchToMail.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseSearchToMail.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              default:
                continue;
            }
          }
          return (IMethodResponse) responseSearchToMail;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse SearchMoviesOnIMDB(string query)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("SearchMoviesOnIMDB", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueBasic((object) query, XmlRpcBasicValueType.String)
        });
        OSHConsole.WriteLine("Sending SearchMoviesOnIMDB request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "SearchMoviesOnIMDB call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseMovieSearch responseMovieSearch = new MethodResponseMovieSearch();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct.Members)
          {
            if (xmlRpcStructMember1.Name == "status")
            {
              responseMovieSearch.Status = (string) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Status= " + responseMovieSearch.Status, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "seconds")
            {
              responseMovieSearch.Seconds = (double) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Seconds= " + (object) responseMovieSearch.Seconds, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "data")
            {
              if (xmlRpcStructMember1.Data is XmlRpcValueArray)
              {
                OSHConsole.WriteLine("Search results:", DebugCode.None);
                foreach (IXmlRpcValue ixmlRpcValue in ((XmlRpcValueArray) xmlRpcStructMember1.Data).Values)
                {
                  if (ixmlRpcValue != null && ixmlRpcValue is XmlRpcValueStruct)
                  {
                    MovieSearchResult movieSearchResult = new MovieSearchResult();
                    foreach (XmlRpcStructMember xmlRpcStructMember2 in ((XmlRpcValueStruct) ixmlRpcValue).Members)
                    {
                      switch (xmlRpcStructMember2.Name)
                      {
                        case "id":
                          movieSearchResult.ID = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        case "title":
                          movieSearchResult.Title = (string) xmlRpcStructMember2.Data.Data;
                          continue;
                        default:
                          continue;
                      }
                    }
                    responseMovieSearch.Results.Add(movieSearchResult);
                    OSHConsole.WriteLine(">" + movieSearchResult.ToString(), DebugCode.None);
                  }
                }
              }
              else
                OSHConsole.WriteLine("Data= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.Warning);
            }
          }
          return (IMethodResponse) responseMovieSearch;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse GetIMDBMovieDetails(string imdbid)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("GetIMDBMovieDetails", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN),
          (IXmlRpcValue) new XmlRpcValueBasic(imdbid)
        });
        OSHConsole.WriteLine("Sending GetIMDBMovieDetails request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "GetIMDBMovieDetails call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseMovieDetails responseMovieDetails = new MethodResponseMovieDetails();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct.Members)
          {
            if (xmlRpcStructMember1.Name == "status")
            {
              responseMovieDetails.Status = (string) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Status= " + responseMovieDetails.Status, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "seconds")
            {
              responseMovieDetails.Seconds = (double) xmlRpcStructMember1.Data.Data;
              OSHConsole.WriteLine("Seconds= " + (object) responseMovieDetails.Seconds, DebugCode.None);
            }
            else if (xmlRpcStructMember1.Name == "data")
            {
              if (xmlRpcStructMember1.Data is XmlRpcValueStruct)
              {
                OSHConsole.WriteLine("Details result:", DebugCode.None);
                foreach (XmlRpcStructMember xmlRpcStructMember2 in ((XmlRpcValueStruct) xmlRpcStructMember1.Data).Members)
                {
                  switch (xmlRpcStructMember2.Name)
                  {
                    case "id":
                      responseMovieDetails.ID = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "title":
                      responseMovieDetails.Title = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "year":
                      responseMovieDetails.Year = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "cover":
                      responseMovieDetails.CoverLink = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "duration":
                      responseMovieDetails.Duration = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "tagline":
                      responseMovieDetails.Tagline = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "plot":
                      responseMovieDetails.Plot = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "goofs":
                      responseMovieDetails.Goofs = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "trivia":
                      responseMovieDetails.Trivia = xmlRpcStructMember2.Data.Data.ToString();
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                      continue;
                    case "cast":
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= ", DebugCode.None);
                      using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember2.Data).Members.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcStructMember current = enumerator.Current;
                          responseMovieDetails.Cast.Add(current.Data.Data.ToString());
                          OSHConsole.WriteLine("  >" + current.Data.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    case "directors":
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= ", DebugCode.None);
                      using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember2.Data).Members.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcStructMember current = enumerator.Current;
                          responseMovieDetails.Directors.Add(current.Data.Data.ToString());
                          OSHConsole.WriteLine("  >" + current.Data.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    case "writers":
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= ", DebugCode.None);
                      using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember2.Data).Members.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcStructMember current = enumerator.Current;
                          responseMovieDetails.Writers.Add(current.Data.Data.ToString());
                          OSHConsole.WriteLine("+->" + current.Data.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    case "awards":
                      using (List<IXmlRpcValue>.Enumerator enumerator = ((XmlRpcValueArray) xmlRpcStructMember2.Data).Values.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcValueBasic xmlRpcValueBasic = (XmlRpcValueBasic) enumerator.Current;
                          responseMovieDetails.Awards.Add(xmlRpcValueBasic.Data.ToString());
                          OSHConsole.WriteLine("  >" + xmlRpcValueBasic.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    case "genres":
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= ", DebugCode.None);
                      using (List<IXmlRpcValue>.Enumerator enumerator = ((XmlRpcValueArray) xmlRpcStructMember2.Data).Values.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcValueBasic xmlRpcValueBasic = (XmlRpcValueBasic) enumerator.Current;
                          responseMovieDetails.Genres.Add(xmlRpcValueBasic.Data.ToString());
                          OSHConsole.WriteLine("  >" + xmlRpcValueBasic.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    case "country":
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= ", DebugCode.None);
                      using (List<IXmlRpcValue>.Enumerator enumerator = ((XmlRpcValueArray) xmlRpcStructMember2.Data).Values.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcValueBasic xmlRpcValueBasic = (XmlRpcValueBasic) enumerator.Current;
                          responseMovieDetails.Country.Add(xmlRpcValueBasic.Data.ToString());
                          OSHConsole.WriteLine("  >" + xmlRpcValueBasic.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    case "language":
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= ", DebugCode.None);
                      using (List<IXmlRpcValue>.Enumerator enumerator = ((XmlRpcValueArray) xmlRpcStructMember2.Data).Values.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcValueBasic xmlRpcValueBasic = (XmlRpcValueBasic) enumerator.Current;
                          responseMovieDetails.Language.Add(xmlRpcValueBasic.Data.ToString());
                          OSHConsole.WriteLine("  >" + xmlRpcValueBasic.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    case "certification":
                      OSHConsole.WriteLine(">" + xmlRpcStructMember2.Name + "= ", DebugCode.None);
                      using (List<IXmlRpcValue>.Enumerator enumerator = ((XmlRpcValueArray) xmlRpcStructMember2.Data).Values.GetEnumerator())
                      {
                        while (enumerator.MoveNext())
                        {
                          XmlRpcValueBasic xmlRpcValueBasic = (XmlRpcValueBasic) enumerator.Current;
                          responseMovieDetails.Certification.Add(xmlRpcValueBasic.Data.ToString());
                          OSHConsole.WriteLine("  >" + xmlRpcValueBasic.Data.ToString(), DebugCode.None);
                        }
                        continue;
                      }
                    default:
                      continue;
                  }
                }
              }
              else
                OSHConsole.WriteLine("Data= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.Warning);
            }
          }
          return (IMethodResponse) responseMovieDetails;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse InsertMovie(string movieName, string movieyear)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("InsertMovie", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("moviename", (IXmlRpcValue) new XmlRpcValueBasic(movieName)),
              new XmlRpcStructMember("movieyear", (IXmlRpcValue) new XmlRpcValueBasic(movieyear))
            }
          }
        });
        OSHConsole.WriteLine("Sending InsertMovie request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "InsertMovie call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseInsertMovie responseInsertMovie = new MethodResponseInsertMovie();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            if (xmlRpcStructMember.Name == "status")
            {
              responseInsertMovie.Status = (string) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine("Status= " + responseInsertMovie.Status, DebugCode.None);
            }
            else if (xmlRpcStructMember.Name == "seconds")
            {
              responseInsertMovie.Seconds = (double) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine("Seconds= " + (object) responseInsertMovie.Seconds, DebugCode.None);
            }
            else if (xmlRpcStructMember.Name == "id")
            {
              responseInsertMovie.ID = (string) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine("ID= " + (object) responseInsertMovie.Seconds, DebugCode.None);
            }
          }
          return (IMethodResponse) responseInsertMovie;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse InsertMovieHash(InsertMovieHashParameters[] parameters)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        List<IXmlRpcValue> parameters1 = new List<IXmlRpcValue>();
        parameters1.Add((IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String));
        foreach (InsertMovieHashParameters movieHashParameters in parameters)
          parameters1.Add((IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("moviehash", (IXmlRpcValue) new XmlRpcValueBasic(movieHashParameters.moviehash)),
              new XmlRpcStructMember("moviebytesize", (IXmlRpcValue) new XmlRpcValueBasic(movieHashParameters.moviebytesize)),
              new XmlRpcStructMember("imdbid", (IXmlRpcValue) new XmlRpcValueBasic(movieHashParameters.imdbid)),
              new XmlRpcStructMember("movietimems", (IXmlRpcValue) new XmlRpcValueBasic(movieHashParameters.movietimems)),
              new XmlRpcStructMember("moviefps", (IXmlRpcValue) new XmlRpcValueBasic(movieHashParameters.moviefps)),
              new XmlRpcStructMember("moviefilename", (IXmlRpcValue) new XmlRpcValueBasic(movieHashParameters.moviefilename))
            }
          });
        XmlRpcMethodCall method = new XmlRpcMethodCall("InsertMovieHash", parameters1);
        OSHConsole.WriteLine("Sending InsertMovieHash request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "InsertMovieHash call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseInsertMovieHash responseInsertMovieHash = new MethodResponseInsertMovieHash();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseInsertMovieHash.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseInsertMovieHash.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                using (List<XmlRpcStructMember>.Enumerator enumerator1 = ((XmlRpcValueStruct) xmlRpcStructMember.Data).Members.GetEnumerator())
                {
                  while (enumerator1.MoveNext())
                  {
                    XmlRpcStructMember current1 = enumerator1.Current;
                    switch (current1.Name)
                    {
                      case "accepted_moviehashes":
                        using (List<IXmlRpcValue>.Enumerator enumerator2 = ((XmlRpcValueArray) current1.Data).Values.GetEnumerator())
                        {
                          while (enumerator2.MoveNext())
                          {
                            IXmlRpcValue current2 = enumerator2.Current;
                            if (current2 is XmlRpcValueBasic)
                              responseInsertMovieHash.accepted_moviehashes.Add(current2.Data.ToString());
                          }
                          continue;
                        }
                      case "new_imdbs":
                        using (List<IXmlRpcValue>.Enumerator enumerator2 = ((XmlRpcValueArray) current1.Data).Values.GetEnumerator())
                        {
                          while (enumerator2.MoveNext())
                          {
                            IXmlRpcValue current2 = enumerator2.Current;
                            if (current2 is XmlRpcValueBasic)
                              responseInsertMovieHash.new_imdbs.Add(current2.Data.ToString());
                          }
                          continue;
                        }
                      default:
                        continue;
                    }
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseInsertMovieHash;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse ServerInfo()
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("ServerInfo", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.XML_PRC_USERAGENT, XmlRpcBasicValueType.String)
        });
        OSHConsole.WriteLine("Sending ServerInfo request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "ServerInfo call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseServerInfo responseServerInfo = new MethodResponseServerInfo();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseServerInfo.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseServerInfo.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "xmlrpc_version":
                responseServerInfo.xmlrpc_version = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "xmlrpc_url":
                responseServerInfo.xmlrpc_url = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "application":
                responseServerInfo.application = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "contact":
                responseServerInfo.contact = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "website_url":
                responseServerInfo.website_url = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "users_online_total":
                responseServerInfo.users_online_total = (int) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "users_online_program":
                responseServerInfo.users_online_program = (int) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "users_loggedin":
                responseServerInfo.users_loggedin = (int) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "users_max_alltime":
                responseServerInfo.users_max_alltime = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "users_registered":
                responseServerInfo.users_registered = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "subs_downloads":
                responseServerInfo.subs_downloads = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "subs_subtitle_files":
                responseServerInfo.subs_subtitle_files = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "movies_total":
                responseServerInfo.movies_total = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "movies_aka":
                responseServerInfo.movies_aka = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "total_subtitles_languages":
                responseServerInfo.total_subtitles_languages = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "last_update_strings":
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + ":", DebugCode.None);
                using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember.Data).Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    responseServerInfo.last_update_strings.Add(current.Name + " [" + current.Data.Data.ToString() + "]");
                    OSHConsole.WriteLine("  >" + current.Name + "= " + current.Data.Data.ToString(), DebugCode.None);
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseServerInfo;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse ReportWrongMovieHash(string IDSubMovieFile)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("ReportWrongMovieHash", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueBasic((object) IDSubMovieFile, XmlRpcBasicValueType.String)
        });
        OSHConsole.WriteLine("Sending ReportWrongMovieHash request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "ReportWrongMovieHash call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseReportWrongMovieHash reportWrongMovieHash = new MethodResponseReportWrongMovieHash();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                reportWrongMovieHash.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                reportWrongMovieHash.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              default:
                continue;
            }
          }
          return (IMethodResponse) reportWrongMovieHash;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse ReportWrongImdbMovie(string moviehash, string moviebytesize, string imdbid)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("ReportWrongImdbMovie", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("moviehash", (IXmlRpcValue) new XmlRpcValueBasic(moviehash)),
              new XmlRpcStructMember("moviebytesize", (IXmlRpcValue) new XmlRpcValueBasic(moviebytesize)),
              new XmlRpcStructMember("imdbid", (IXmlRpcValue) new XmlRpcValueBasic(imdbid))
            }
          }
        });
        OSHConsole.WriteLine("Sending ReportWrongImdbMovie request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "ReportWrongImdbMovie call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseAddComment responseAddComment = new MethodResponseAddComment();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseAddComment.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseAddComment.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              default:
                continue;
            }
          }
          return (IMethodResponse) responseAddComment;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse SubtitlesVote(int idsubtitle, int score)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("SubtitlesVote", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("idsubtitle", (IXmlRpcValue) new XmlRpcValueBasic(idsubtitle)),
              new XmlRpcStructMember("score", (IXmlRpcValue) new XmlRpcValueBasic(score))
            }
          }
        });
        OSHConsole.WriteLine("Sending SubtitlesVote request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "SubtitlesVote call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseSubtitlesVote responseSubtitlesVote = new MethodResponseSubtitlesVote();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseSubtitlesVote.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseSubtitlesVote.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember.Data).Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    OSHConsole.WriteLine("  >" + current.Name + "= " + current.Data.Data.ToString(), DebugCode.None);
                    switch (current.Name)
                    {
                      case "SubRating":
                        responseSubtitlesVote.SubRating = current.Data.Data.ToString();
                        continue;
                      case "SubSumVotes":
                        responseSubtitlesVote.SubSumVotes = current.Data.Data.ToString();
                        continue;
                      case "IDSubtitle":
                        responseSubtitlesVote.IDSubtitle = current.Data.Data.ToString();
                        continue;
                      default:
                        continue;
                    }
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseSubtitlesVote;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse AddComment(int idsubtitle, string comment, int badsubtitle)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("AddComment", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("idsubtitle", (IXmlRpcValue) new XmlRpcValueBasic(idsubtitle)),
              new XmlRpcStructMember("comment", (IXmlRpcValue) new XmlRpcValueBasic(comment)),
              new XmlRpcStructMember("badsubtitle", (IXmlRpcValue) new XmlRpcValueBasic(badsubtitle))
            }
          }
        });
        OSHConsole.WriteLine("Sending AddComment request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "AddComment call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseAddComment responseAddComment = new MethodResponseAddComment();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseAddComment.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseAddComment.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              default:
                continue;
            }
          }
          return (IMethodResponse) responseAddComment;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse AddRequest(string sublanguageid, string idmovieimdb, string comment)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("AddRequest", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String),
          (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("sublanguageid", (IXmlRpcValue) new XmlRpcValueBasic(sublanguageid)),
              new XmlRpcStructMember("idmovieimdb", (IXmlRpcValue) new XmlRpcValueBasic(idmovieimdb)),
              new XmlRpcStructMember("comment", (IXmlRpcValue) new XmlRpcValueBasic(comment))
            }
          }
        });
        OSHConsole.WriteLine("Sending AddRequest request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "AddRequest call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseAddRequest responseAddRequest = new MethodResponseAddRequest();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseAddRequest.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseAddRequest.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember.Data).Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    switch (current.Name)
                    {
                      case "request_url":
                        responseAddRequest.request_url = current.Data.Data.ToString();
                        OSHConsole.WriteLine(">" + current.Name + "= " + current.Data.Data.ToString(), DebugCode.None);
                        continue;
                      default:
                        continue;
                    }
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseAddRequest;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse GetSubLanguages(string language)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("GetSubLanguages", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN),
          (IXmlRpcValue) new XmlRpcValueBasic(language)
        });
        OSHConsole.WriteLine("Sending GetSubLanguages request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "GetSubLanguages call failed !");
          XmlRpcValueStruct xmlRpcValueStruct1 = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseGetSubLanguages responseGetSubLanguages = new MethodResponseGetSubLanguages();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct1.Members)
          {
            switch (xmlRpcStructMember1.Name)
            {
              case "status":
                responseGetSubLanguages.Status = (string) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseGetSubLanguages.Seconds = (double) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                using (List<IXmlRpcValue>.Enumerator enumerator = ((XmlRpcValueArray) xmlRpcStructMember1.Data).Values.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    IXmlRpcValue current = enumerator.Current;
                    if (current is XmlRpcValueStruct)
                    {
                      XmlRpcValueStruct xmlRpcValueStruct2 = (XmlRpcValueStruct) current;
                      SubtitleLanguage subtitleLanguage = new SubtitleLanguage();
                      OSHConsole.WriteLine(">SubLanguage:", DebugCode.None);
                      foreach (XmlRpcStructMember xmlRpcStructMember2 in xmlRpcValueStruct2.Members)
                      {
                        OSHConsole.WriteLine("  >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                        switch (xmlRpcStructMember2.Name)
                        {
                          case "SubLanguageID":
                            subtitleLanguage.SubLanguageID = xmlRpcStructMember2.Data.Data.ToString();
                            continue;
                          case "LanguageName":
                            subtitleLanguage.LanguageName = xmlRpcStructMember2.Data.Data.ToString();
                            continue;
                          case "ISO639":
                            subtitleLanguage.ISO639 = xmlRpcStructMember2.Data.Data.ToString();
                            continue;
                          default:
                            continue;
                        }
                      }
                      responseGetSubLanguages.Languages.Add(subtitleLanguage);
                    }
                    else
                      OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString() + " [Struct expected !]", DebugCode.Warning);
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseGetSubLanguages;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse DetectLanguage(string[] texts, Encoding encodingUsed)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        List<IXmlRpcValue> parameters = new List<IXmlRpcValue>();
        parameters.Add((IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String));
        List<string> list = new List<string>();
        foreach (string s in texts)
        {
          Stream dataToCompress = (Stream) new MemoryStream();
          byte[] bytes = encodingUsed.GetBytes(s);
          dataToCompress.Write(bytes, 0, bytes.Length);
          dataToCompress.Position = 0L;
          byte[] inArray = Utilities.Compress(dataToCompress);
          list.Add(Convert.ToBase64String(inArray));
        }
        parameters.Add((IXmlRpcValue) new XmlRpcValueArray(list.ToArray()));
        XmlRpcMethodCall method = new XmlRpcMethodCall("DetectLanguage", parameters);
        OSHConsole.WriteLine("Sending DetectLanguage request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "DetectLanguage call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseDetectLanguage responseDetectLanguage = new MethodResponseDetectLanguage();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseDetectLanguage.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseDetectLanguage.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                if (xmlRpcStructMember.Data is XmlRpcValueStruct)
                {
                  OSHConsole.WriteLine(">Languages:", DebugCode.None);
                  using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember.Data).Members.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      XmlRpcStructMember current = enumerator.Current;
                      responseDetectLanguage.Results.Add(new DetectLanguageResult()
                      {
                        InputSample = current.Name,
                        LanguageID = current.Data.Data.ToString()
                      });
                      OSHConsole.WriteLine("  >" + current.Name + " (" + current.Data.Data.ToString() + ")", DebugCode.None);
                    }
                    continue;
                  }
                }
                else
                {
                  OSHConsole.WriteLine(">Languages ?? Struct expected but server return another type!!", DebugCode.Warning);
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseDetectLanguage;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse GetAvailableTranslations(string program)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("GetAvailableTranslations", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN),
          (IXmlRpcValue) new XmlRpcValueBasic(program)
        });
        OSHConsole.WriteLine("Sending GetAvailableTranslations request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "GetAvailableTranslations call failed !");
          XmlRpcValueStruct xmlRpcValueStruct1 = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseGetAvailableTranslations availableTranslations = new MethodResponseGetAvailableTranslations();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct1.Members)
          {
            switch (xmlRpcStructMember1.Name)
            {
              case "status":
                availableTranslations.Status = (string) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                availableTranslations.Seconds = (double) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                XmlRpcValueStruct xmlRpcValueStruct2 = (XmlRpcValueStruct) xmlRpcStructMember1.Data;
                OSHConsole.WriteLine(">data:", DebugCode.None);
                using (List<XmlRpcStructMember>.Enumerator enumerator = xmlRpcValueStruct2.Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    if (current.Data is XmlRpcValueStruct)
                    {
                      XmlRpcValueStruct xmlRpcValueStruct3 = (XmlRpcValueStruct) current.Data;
                      GetAvailableTranslationsResult translationsResult = new GetAvailableTranslationsResult();
                      translationsResult.LanguageID = current.Name;
                      OSHConsole.WriteLine("  >LanguageID: " + current.Name, DebugCode.None);
                      foreach (XmlRpcStructMember xmlRpcStructMember2 in xmlRpcValueStruct3.Members)
                      {
                        switch (xmlRpcStructMember2.Name)
                        {
                          case "LastCreated":
                            translationsResult.LastCreated = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("  >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            break;
                          case "StringsNo":
                            translationsResult.StringsNo = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("  >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            break;
                        }
                        availableTranslations.Results.Add(translationsResult);
                      }
                    }
                    else
                      OSHConsole.WriteLine("  >Struct expected !!", DebugCode.Warning);
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) availableTranslations;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse GetTranslation(string iso639, string format, string program)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("GetTranslation", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN),
          (IXmlRpcValue) new XmlRpcValueBasic(iso639),
          (IXmlRpcValue) new XmlRpcValueBasic(format),
          (IXmlRpcValue) new XmlRpcValueBasic(program)
        });
        OSHConsole.WriteLine("Sending GetTranslation request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "GetTranslation call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseGetTranslation responseGetTranslation = new MethodResponseGetTranslation();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseGetTranslation.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseGetTranslation.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                responseGetTranslation.ContentData = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              default:
                continue;
            }
          }
          return (IMethodResponse) responseGetTranslation;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse AutoUpdate(string program)
    {
      XmlRpcMethodCall method = new XmlRpcMethodCall("AutoUpdate", new List<IXmlRpcValue>()
      {
        (IXmlRpcValue) new XmlRpcValueBasic(program)
      });
      OSHConsole.WriteLine("Sending AutoUpdate request to the server ...", DebugCode.Good);
      string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
      if (!streamString.Contains("ERROR:"))
      {
        XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
        if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
          return (IMethodResponse) new MethodResponseError("Fail", "AutoUpdate call failed !");
        XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
        MethodResponseAutoUpdate responseAutoUpdate = new MethodResponseAutoUpdate();
        foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
        {
          switch (xmlRpcStructMember.Name)
          {
            case "status":
              responseAutoUpdate.Status = (string) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            case "seconds":
              responseAutoUpdate.Seconds = (double) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            case "version":
              responseAutoUpdate.version = (string) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            case "url_windows":
              responseAutoUpdate.url_windows = (string) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            case "url_linux":
              responseAutoUpdate.url_linux = (string) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            case "comments":
              responseAutoUpdate.comments = (string) xmlRpcStructMember.Data.Data;
              OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
              continue;
            default:
              continue;
          }
        }
        return (IMethodResponse) responseAutoUpdate;
      }
      else
      {
        OSHConsole.WriteLine(streamString, DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", streamString);
      }
    }

    public static IMethodResponse CheckMovieHash(string[] hashes)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("CheckMovieHash", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN),
          (IXmlRpcValue) new XmlRpcValueArray(hashes)
        });
        OSHConsole.WriteLine("Sending CheckMovieHash request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "CheckMovieHash call failed !");
          XmlRpcValueStruct xmlRpcValueStruct1 = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseCheckMovieHash responseCheckMovieHash = new MethodResponseCheckMovieHash();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct1.Members)
          {
            switch (xmlRpcStructMember1.Name)
            {
              case "status":
                responseCheckMovieHash.Status = (string) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseCheckMovieHash.Seconds = (double) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                XmlRpcValueStruct xmlRpcValueStruct2 = (XmlRpcValueStruct) xmlRpcStructMember1.Data;
                OSHConsole.WriteLine(">Data:", DebugCode.None);
                using (List<XmlRpcStructMember>.Enumerator enumerator = xmlRpcValueStruct2.Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    CheckMovieHashResult checkMovieHashResult = new CheckMovieHashResult();
                    checkMovieHashResult.Name = current.Name;
                    OSHConsole.WriteLine("  >" + checkMovieHashResult.Name + ":", DebugCode.None);
                    foreach (XmlRpcStructMember xmlRpcStructMember2 in ((XmlRpcValueStruct) current.Data).Members)
                    {
                      switch (xmlRpcStructMember2.Name)
                      {
                        case "MovieHash":
                          checkMovieHashResult.MovieHash = xmlRpcStructMember2.Data.Data.ToString();
                          OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                          continue;
                        case "MovieImdbID":
                          checkMovieHashResult.MovieImdbID = xmlRpcStructMember2.Data.Data.ToString();
                          OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                          continue;
                        case "MovieName":
                          checkMovieHashResult.MovieName = xmlRpcStructMember2.Data.Data.ToString();
                          OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                          continue;
                        case "MovieYear":
                          checkMovieHashResult.MovieYear = xmlRpcStructMember2.Data.Data.ToString();
                          OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                          continue;
                        default:
                          continue;
                      }
                    }
                    responseCheckMovieHash.Results.Add(checkMovieHashResult);
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseCheckMovieHash;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse CheckMovieHash2(string[] hashes)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("CheckMovieHash2", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN),
          (IXmlRpcValue) new XmlRpcValueArray(hashes)
        });
        OSHConsole.WriteLine("Sending CheckMovieHash2 request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "CheckMovieHash2 call failed !");
          XmlRpcValueStruct xmlRpcValueStruct1 = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseCheckMovieHash2 responseCheckMovieHash2 = new MethodResponseCheckMovieHash2();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct1.Members)
          {
            switch (xmlRpcStructMember1.Name)
            {
              case "status":
                responseCheckMovieHash2.Status = (string) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseCheckMovieHash2.Seconds = (double) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                XmlRpcValueStruct xmlRpcValueStruct2 = (XmlRpcValueStruct) xmlRpcStructMember1.Data;
                OSHConsole.WriteLine(">Data:", DebugCode.None);
                using (List<XmlRpcStructMember>.Enumerator enumerator = xmlRpcValueStruct2.Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    CheckMovieHash2Result movieHash2Result = new CheckMovieHash2Result();
                    movieHash2Result.Name = current.Name;
                    OSHConsole.WriteLine("  >" + movieHash2Result.Name + ":", DebugCode.None);
                    foreach (XmlRpcValueStruct xmlRpcValueStruct3 in ((XmlRpcValueArray) current.Data).Values)
                    {
                      CheckMovieHash2Data checkMovieHash2Data = new CheckMovieHash2Data();
                      foreach (XmlRpcStructMember xmlRpcStructMember2 in xmlRpcValueStruct3.Members)
                      {
                        switch (xmlRpcStructMember2.Name)
                        {
                          case "MovieHash":
                            checkMovieHash2Data.MovieHash = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          case "MovieImdbID":
                            checkMovieHash2Data.MovieImdbID = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          case "MovieName":
                            checkMovieHash2Data.MovieName = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          case "MovieYear":
                            checkMovieHash2Data.MovieYear = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          case "MovieKind":
                            checkMovieHash2Data.MovieKind = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          case "SeriesSeason":
                            checkMovieHash2Data.SeriesSeason = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          case "SeriesEpisode":
                            checkMovieHash2Data.SeriesEpisode = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          case "SeenCount":
                            checkMovieHash2Data.MovieYear = xmlRpcStructMember2.Data.Data.ToString();
                            OSHConsole.WriteLine("    >" + xmlRpcStructMember2.Name + "= " + xmlRpcStructMember2.Data.Data.ToString(), DebugCode.None);
                            continue;
                          default:
                            continue;
                        }
                      }
                      movieHash2Result.Items.Add(checkMovieHash2Data);
                    }
                    responseCheckMovieHash2.Results.Add(movieHash2Result);
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseCheckMovieHash2;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse CheckSubHash(string[] hashes)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        XmlRpcMethodCall method = new XmlRpcMethodCall("CheckSubHash", new List<IXmlRpcValue>()
        {
          (IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN),
          (IXmlRpcValue) new XmlRpcValueArray(hashes)
        });
        OSHConsole.WriteLine("Sending CheckSubHash request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "CheckSubHash call failed !");
          XmlRpcValueStruct xmlRpcValueStruct = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseCheckSubHash responseCheckSubHash = new MethodResponseCheckSubHash();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseCheckSubHash.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseCheckSubHash.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                OSHConsole.WriteLine(">Data:", DebugCode.None);
                using (List<XmlRpcStructMember>.Enumerator enumerator = ((XmlRpcValueStruct) xmlRpcStructMember.Data).Members.GetEnumerator())
                {
                  while (enumerator.MoveNext())
                  {
                    XmlRpcStructMember current = enumerator.Current;
                    OSHConsole.WriteLine("  >" + current.Name + "= " + current.Data.Data.ToString(), DebugCode.None);
                    responseCheckSubHash.Results.Add(new CheckSubHashResult()
                    {
                      Hash = current.Name,
                      SubID = current.Data.Data.ToString()
                    });
                  }
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) responseCheckSubHash;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse TryUploadSubtitles(TryUploadSubtitlesParameters[] subs)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        List<IXmlRpcValue> parameters = new List<IXmlRpcValue>();
        parameters.Add((IXmlRpcValue) new XmlRpcValueBasic((object) OpenSubtitles.TOKEN, XmlRpcBasicValueType.String));
        XmlRpcValueStruct xmlRpcValueStruct1 = new XmlRpcValueStruct(new List<XmlRpcStructMember>());
        int num = 1;
        foreach (TryUploadSubtitlesParameters subtitlesParameters in subs)
        {
          xmlRpcValueStruct1.Members.Add(new XmlRpcStructMember("cd" + (object) num, (IXmlRpcValue) null)
          {
            Data = (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
            {
              Members = {
                new XmlRpcStructMember("subhash", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.subhash)),
                new XmlRpcStructMember("subfilename", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.subfilename)),
                new XmlRpcStructMember("moviehash", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.moviehash)),
                new XmlRpcStructMember("moviebytesize", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.moviebytesize)),
                new XmlRpcStructMember("moviefps", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.moviefps)),
                new XmlRpcStructMember("movietimems", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.movietimems)),
                new XmlRpcStructMember("movieframes", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.movieframes)),
                new XmlRpcStructMember("moviefilename", (IXmlRpcValue) new XmlRpcValueBasic(subtitlesParameters.moviefilename))
              }
            }
          });
          ++num;
        }
        parameters.Add((IXmlRpcValue) xmlRpcValueStruct1);
        XmlRpcMethodCall method = new XmlRpcMethodCall("TryUploadSubtitles", parameters);
        OSHConsole.WriteLine("Sending TryUploadSubtitles request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "TryUploadSubtitles call failed !");
          XmlRpcValueStruct xmlRpcValueStruct2 = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseTryUploadSubtitles tryUploadSubtitles = new MethodResponseTryUploadSubtitles();
          foreach (XmlRpcStructMember xmlRpcStructMember1 in xmlRpcValueStruct2.Members)
          {
            switch (xmlRpcStructMember1.Name)
            {
              case "status":
                tryUploadSubtitles.Status = (string) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                tryUploadSubtitles.Seconds = (double) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "alreadyindb":
                tryUploadSubtitles.AlreadyInDB = (int) xmlRpcStructMember1.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember1.Name + "= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                if (xmlRpcStructMember1.Data is XmlRpcValueArray)
                {
                  OSHConsole.WriteLine("Results: ", DebugCode.None);
                  using (List<IXmlRpcValue>.Enumerator enumerator = ((XmlRpcValueArray) xmlRpcStructMember1.Data).Values.GetEnumerator())
                  {
                    while (enumerator.MoveNext())
                    {
                      IXmlRpcValue current = enumerator.Current;
                      if (current != null && current is XmlRpcValueStruct)
                      {
                        SubtitleSearchResult subtitleSearchResult = new SubtitleSearchResult();
                        foreach (XmlRpcStructMember xmlRpcStructMember2 in ((XmlRpcValueStruct) current).Members)
                        {
                          switch (xmlRpcStructMember2.Name)
                          {
                            case "IDMovie":
                              subtitleSearchResult.IDMovie = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "IDMovieImdb":
                              subtitleSearchResult.IDMovieImdb = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "IDSubMovieFile":
                              subtitleSearchResult.IDSubMovieFile = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "IDSubtitle":
                              subtitleSearchResult.IDSubtitle = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "IDSubtitleFile":
                              subtitleSearchResult.IDSubtitleFile = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "ISO639":
                              subtitleSearchResult.ISO639 = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "LanguageName":
                              subtitleSearchResult.LanguageName = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieByteSize":
                              subtitleSearchResult.MovieByteSize = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieHash":
                              subtitleSearchResult.MovieHash = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieImdbRating":
                              subtitleSearchResult.MovieImdbRating = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieName":
                              subtitleSearchResult.MovieName = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieNameEng":
                              subtitleSearchResult.MovieNameEng = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieReleaseName":
                              subtitleSearchResult.MovieReleaseName = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieTimeMS":
                              subtitleSearchResult.MovieTimeMS = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "MovieYear":
                              subtitleSearchResult.MovieYear = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubActualCD":
                              subtitleSearchResult.SubActualCD = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubAddDate":
                              subtitleSearchResult.SubAddDate = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubAuthorComment":
                              subtitleSearchResult.SubAuthorComment = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubBad":
                              subtitleSearchResult.SubBad = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubDownloadLink":
                              subtitleSearchResult.SubDownloadLink = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubDownloadsCnt":
                              subtitleSearchResult.SubDownloadsCnt = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubFileName":
                              subtitleSearchResult.SubFileName = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubFormat":
                              subtitleSearchResult.SubFormat = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubHash":
                              subtitleSearchResult.SubHash = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubLanguageID":
                              subtitleSearchResult.SubLanguageID = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubRating":
                              subtitleSearchResult.SubRating = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubSize":
                              subtitleSearchResult.SubSize = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "SubSumCD":
                              subtitleSearchResult.SubSumCD = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "UserID":
                              subtitleSearchResult.UserID = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "UserNickName":
                              subtitleSearchResult.UserNickName = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            case "ZipDownloadLink":
                              subtitleSearchResult.ZipDownloadLink = xmlRpcStructMember2.Data.Data.ToString();
                              continue;
                            default:
                              continue;
                          }
                        }
                        tryUploadSubtitles.Results.Add(subtitleSearchResult);
                        OSHConsole.WriteLine(">" + subtitleSearchResult.ToString(), DebugCode.None);
                      }
                    }
                    continue;
                  }
                }
                else
                {
                  OSHConsole.WriteLine("Data= " + xmlRpcStructMember1.Data.Data.ToString(), DebugCode.Warning);
                  continue;
                }
              default:
                continue;
            }
          }
          return (IMethodResponse) tryUploadSubtitles;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }

    public static IMethodResponse UploadSubtitles(UploadSubtitleInfoParameters info)
    {
      if (OpenSubtitles.TOKEN == "")
      {
        OSHConsole.WriteLine("Can't do this call, 'token' value not set. Please use Log In method first.", DebugCode.Error);
        return (IMethodResponse) new MethodResponseError("Fail", "Can't do this call, 'token' value not set. Please use Log In method first.");
      }
      else
      {
        List<IXmlRpcValue> parameters = new List<IXmlRpcValue>();
        parameters.Add((IXmlRpcValue) new XmlRpcValueBasic(OpenSubtitles.TOKEN));
        XmlRpcValueStruct xmlRpcValueStruct1 = new XmlRpcValueStruct(new List<XmlRpcStructMember>());
        xmlRpcValueStruct1.Members.Add(new XmlRpcStructMember("baseinfo", (IXmlRpcValue) null)
        {
          Data = (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
          {
            Members = {
              new XmlRpcStructMember("idmovieimdb", (IXmlRpcValue) new XmlRpcValueBasic(info.idmovieimdb)),
              new XmlRpcStructMember("sublanguageid", (IXmlRpcValue) new XmlRpcValueBasic(info.sublanguageid)),
              new XmlRpcStructMember("moviereleasename", (IXmlRpcValue) new XmlRpcValueBasic(info.moviereleasename)),
              new XmlRpcStructMember("movieaka", (IXmlRpcValue) new XmlRpcValueBasic(info.movieaka)),
              new XmlRpcStructMember("subauthorcomment", (IXmlRpcValue) new XmlRpcValueBasic(info.subauthorcomment))
            }
          }
        });
        int num = 1;
        foreach (UploadSubtitleParameters subtitleParameters in info.CDS)
        {
          xmlRpcValueStruct1.Members.Add(new XmlRpcStructMember("cd" + (object) num, (IXmlRpcValue) null)
          {
            Data = (IXmlRpcValue) new XmlRpcValueStruct(new List<XmlRpcStructMember>())
            {
              Members = {
                new XmlRpcStructMember("subhash", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.subhash)),
                new XmlRpcStructMember("subfilename", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.subfilename)),
                new XmlRpcStructMember("moviehash", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.moviehash)),
                new XmlRpcStructMember("moviebytesize", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.moviebytesize)),
                new XmlRpcStructMember("moviefps", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.moviefps)),
                new XmlRpcStructMember("movietimems", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.movietimems)),
                new XmlRpcStructMember("movieframes", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.movieframes)),
                new XmlRpcStructMember("moviefilename", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.moviefilename)),
                new XmlRpcStructMember("subcontent", (IXmlRpcValue) new XmlRpcValueBasic(subtitleParameters.subcontent))
              }
            }
          });
          ++num;
        }
        parameters.Add((IXmlRpcValue) xmlRpcValueStruct1);
        XmlRpcMethodCall method = new XmlRpcMethodCall("UploadSubtitles", parameters);
        OSHConsole.WriteLine("Sending UploadSubtitles request to the server ...", DebugCode.Good);
        string streamString = Utilities.GetStreamString(Utilities.SendRequest(XmlRpcGenerator.Generate(method), OpenSubtitles.XML_PRC_USERAGENT));
        if (!streamString.Contains("ERROR:"))
        {
          XmlRpcMethodCall[] xmlRpcMethodCallArray = XmlRpcGenerator.DecodeMethodResponse(streamString);
          if (xmlRpcMethodCallArray.Length <= 0 || xmlRpcMethodCallArray[0].Parameters.Count <= 0)
            return (IMethodResponse) new MethodResponseError("Fail", "UploadSubtitles call failed !");
          XmlRpcValueStruct xmlRpcValueStruct2 = (XmlRpcValueStruct) xmlRpcMethodCallArray[0].Parameters[0];
          MethodResponseUploadSubtitles responseUploadSubtitles = new MethodResponseUploadSubtitles();
          foreach (XmlRpcStructMember xmlRpcStructMember in xmlRpcValueStruct2.Members)
          {
            switch (xmlRpcStructMember.Name)
            {
              case "status":
                responseUploadSubtitles.Status = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "seconds":
                responseUploadSubtitles.Seconds = (double) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "data":
                responseUploadSubtitles.Data = (string) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              case "subtitles":
                responseUploadSubtitles.SubTitles = (bool) xmlRpcStructMember.Data.Data;
                OSHConsole.WriteLine(">" + xmlRpcStructMember.Name + "= " + xmlRpcStructMember.Data.Data.ToString(), DebugCode.None);
                continue;
              default:
                continue;
            }
          }
          return (IMethodResponse) responseUploadSubtitles;
        }
        else
        {
          OSHConsole.WriteLine(streamString, DebugCode.Error);
          return (IMethodResponse) new MethodResponseError("Fail", streamString);
        }
      }
    }
  }
}
