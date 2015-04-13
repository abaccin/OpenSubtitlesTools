  using OpenSubtitlesHandler;
  using System;
  using System.Collections.Generic;
  using System.IO;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

namespace VideoRenamer
{

    internal class OpenSubs : IDisposable
    {
      private const string UserAgent = "SubtitleTools";
      public OpenSubs()
      {
        OpenSubtitles.SetUserAgent(UserAgent);
        Login();

      }

      private void Login()
      {
        IMethodResponse imethodResponse = OpenSubtitles.LogIn(String.Empty, String.Empty, UserAgent);
        if (imethodResponse.Status == "200 OK")
        {

        }
      }

      private void LogOut()
      {
        IMethodResponse imethodResponse = OpenSubtitles.LogOut();
        if (imethodResponse.Status == "200 OK")
        {

        }
      }

      internal MethodResponseSubtitleSearch SearchSubtitle(FileInfo videoFile, string languageId = "eng")
      {
        byte[] hashsize = MovieHasher.ComputeMovieHash(videoFile.FullName);
        string hashcode = MovieHasher.ToHexadecimal(hashsize);
        var re = new SubtitleSearchParameters();
        re.MovieByteSize = videoFile.Length;
        re.MovieHash = hashcode;
        re.SubLangaugeID = languageId;
        re.IMDbID = "";
        IMethodResponse imethodResponse = OpenSubtitles.SearchSubtitles(
          new SubtitleSearchParameters[] { re }
          );

        if (imethodResponse.Status == "200 OK")
          if (imethodResponse is MethodResponseSubtitleSearch)
            return imethodResponse as MethodResponseSubtitleSearch;

        return null;
      }
      private MethodResponseMovieSearch SearchMoviesOnIMDB(string movieTitle)
      {
        IMethodResponse imethodResponse = OpenSubtitles.SearchMoviesOnIMDB(movieTitle);
        if (imethodResponse is MethodResponseMovieSearch)
          return imethodResponse as MethodResponseMovieSearch;

        return null;

      }

      private MethodResponseMovieDetails GetIMDBMovieDetails(string movieIMBID)
      {
        IMethodResponse imdbMovieDetails = OpenSubtitles.GetIMDBMovieDetails(movieIMBID);
        if (imdbMovieDetails is MethodResponseMovieDetails)
          return imdbMovieDetails as MethodResponseMovieDetails;

        return null;
      }

      internal void DownloadSubtitle(FileInfo videoFile, string languageId = "eng")
      {
        var searchRes = SearchSubtitle(videoFile, languageId);
        if (searchRes == null || !searchRes.Results.Any())
          return;

        DownloadSubtitle(videoFile, searchRes);

      }

      internal void DownloadSubtitle(FileInfo videoFile, MethodResponseSubtitleSearch methodResponseSubtitleSearch)
      {
        var mostDownloaded = methodResponseSubtitleSearch.Results.OrderByDescending(x => x.SubDownloadsCnt).First();
        var imethodResponse = OpenSubtitles.DownloadSubtitles(new int[] { int.Parse(mostDownloaded.IDSubtitleFile) });
        if (!(imethodResponse is MethodResponseSubtitleDownload))
          return;

        var response = imethodResponse as MethodResponseSubtitleDownload;
        var subtitleDownloadResult = response.Results.First();
        byte[] buffer = Utilities.Decompress((Stream)new MemoryStream(Convert.FromBase64String(subtitleDownloadResult.Data)));
        string str = videoFile.FullName.Replace(videoFile.Extension, ".srt");
        var stream = new FileStream(str, FileMode.Create, FileAccess.Write);
        stream.Write(buffer, 0, buffer.Length);
        stream.Close();

      }

      internal void RenameAndDownloadSub(FileInfo videoFile, string languageId = "eng")
      {
        var searchRes = SearchSubtitle(videoFile, languageId);
        if (searchRes == null || !searchRes.Results.Any())
          return;

        var mostDownloaded = searchRes.Results.OrderByDescending(x => x.SubDownloadsCnt).First();

        string newFilename = System.IO.Path.Combine(videoFile.DirectoryName, mostDownloaded.MovieName) + videoFile.Extension;

        System.IO.File.Move(videoFile.FullName, newFilename);
        DownloadSubtitle(new FileInfo(newFilename), searchRes);

      }

      public void Dispose()
      {
        LogOut();
      }
    }
  }


