using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Isam.Esent.Collections.Generic;
using System.Linq.Expressions;
using OpenSubtitlesHandler;

namespace VideoRenamer
{
  internal class Renamer
  {
    private OpenSubs _openSubs { get; set; }
    private static IProgress<Progress> ProgressReporter { get; set; }
    public static Task RenameFilesInDirAsync(string directory, Func<MovieData, string> nameFormatter, IProgress<Progress> progress)
    {
      return Task.Run(() => new Renamer().RenameFilesInDir(directory, nameFormatter, progress));
    }

    public Renamer()
    {
      _openSubs = new OpenSubs();
    }
    public void RenameFilesInDir(string directory, Func<MovieData, string> nameFormatter, IProgress<Progress> progress)
    {

      InitializeDb(directory);

      ProgressReporter = progress;
      var lstFiles = GetMovies(directory).ToList();

      foreach (var item in GetMovieNames(lstFiles, nameFormatter))
        RenameMovie(item);

      MovieCache.Dispose();
    }

    private string CleanFileName(string fileName)
    {
      return Path.GetInvalidFileNameChars().Aggregate(fileName, (current, c) => current.Replace(c.ToString(), string.Empty));
    }

    private void RenameMovie(UpdatedMovieInfo movieInfo)
    {

      if (movieInfo.MovieFileInfo.FullName != movieInfo.NewMovieInfo.FullName)
      {
        ProgressReporter.Report(Progress.New().SetOldFileName(movieInfo.MovieFileInfo.FullName).SetNewFileName(movieInfo.NewMovieInfo.FullName));
        System.IO.File.Move(movieInfo.MovieFileInfo.FullName, movieInfo.NewMovieInfo.FullName);
      }

      if (movieInfo.Subtitle != movieInfo.NewSubtitle)
        if (File.Exists(movieInfo.Subtitle))
          System.IO.File.Move(movieInfo.Subtitle, movieInfo.NewSubtitle);
    }

    private FileInfo GetNewMovieName(string oldFullPath, string newMovieName)
    {
      var flNfo = new System.IO.FileInfo(oldFullPath);
      return new FileInfo(Path.Combine(flNfo.DirectoryName, newMovieName) + flNfo.Extension);
    }

    private List<MovieInfo> GetMovies(string directory)
    {
      var lRet = new List<MovieInfo>();

      string[] extensions = { ".mp4", ".avi", ".mkv" };
      var lst = System.IO.Directory.GetFiles(directory)
        .Where(x => extensions.Contains(Path.GetExtension(x).ToLower())).ToList().OrderBy(x => x);

      foreach (var item in lst)
      {
        var obj = new MovieInfo();
        obj.MovieFileInfo = new System.IO.FileInfo(item);
        string sub = obj.MovieFileInfo.Name.Replace(obj.MovieFileInfo.Extension, ".srt");
        sub = Path.Combine(obj.MovieFileInfo.DirectoryName, sub);

        obj.Subtitle = sub;

        lRet.Add(obj);
      }

      ProgressReporter.Report(Progress.New().SetMessage("Found " + lRet.Count().ToString() + " movies"));
      return lRet;
    }

    private class MovieInfo
    {
      public System.IO.FileInfo MovieFileInfo { get; set; }
      public string Subtitle { get; set; }
    }

    private class UpdatedMovieInfo : MovieInfo
    {
      public System.IO.FileInfo NewMovieInfo { get; set; }
      public string NewSubtitle { get; set; }
    }
    private List<UpdatedMovieInfo> GetMovieNames(List<MovieInfo> movies, Func<MovieData, string> nameFormatter)
    {
      var tasks = new List<Task<UpdatedMovieInfo>>();
      var batches = movies.Batch(100);
      var lstResult = new List<UpdatedMovieInfo>();
      foreach (var batch in batches)
      {
        tasks.Clear();
        foreach (var item in batch)
          tasks.Add(GetMovieNameAsync(item, nameFormatter));

        Task.WaitAll(tasks.ToArray());

        foreach (var item in tasks)
          lstResult.Add(item.Result);

      }

      return lstResult;
    }

    private Task<UpdatedMovieInfo> GetMovieNameAsync(MovieInfo movie, Func<MovieData, string> nameFormatter)
    {
      return Task.Run<UpdatedMovieInfo>(() => GetMovieName(movie, nameFormatter));
    }

    private UpdatedMovieInfo GetMovieName(MovieInfo movie, Func<MovieData, string> nameFormatter)
    {

      var obj = GetMovieName(movie.MovieFileInfo.FullName);

      if (obj.HasValue)
      {

        var newName = GetNewMovieName(movie.MovieFileInfo.FullName, CleanFileName(nameFormatter(obj.Value)));

        return new UpdatedMovieInfo
        {
          MovieFileInfo = movie.MovieFileInfo,
          Subtitle = movie.Subtitle,
          NewMovieInfo = newName,
          NewSubtitle = newName.FullName.Replace(newName.Extension, ".srt")

        };
      }
      else
      {
        return new UpdatedMovieInfo
        {
          MovieFileInfo = movie.MovieFileInfo,
          Subtitle = movie.Subtitle,
          NewMovieInfo = movie.MovieFileInfo,
          NewSubtitle = movie.Subtitle
        };
      }

    }

    private MovieData? GetMovieName(string path)
    {

      var res = _openSubs.SearchSubtitle(new FileInfo(path));
      if (res != null && res.Results.Any())
      {
        var obj = AutoMapper.Mapper.Map<SubtitleSearchResult, MovieData>(res.Results.First());

        return obj;
      }

      return null;
    }

    private PersistentDictionary<string, MovieData> MovieCache { get; set; }

    private void InitializeDb(string baseDir)
    {
      MovieCache = new PersistentDictionary<string, MovieData>(GetDbDirectory(baseDir));

    }

    private string GetDbDirectory(string baseDir)
    {
      string dataDir = System.IO.Path.Combine(baseDir, "Data");
      if (!System.IO.Directory.Exists(dataDir))
        System.IO.Directory.CreateDirectory(dataDir);

      return dataDir;
    }

  }


}

