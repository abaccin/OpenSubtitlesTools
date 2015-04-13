using AutoMapper;
using OpenSubtitlesHandler;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace VideoRenamer
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
      protected override void OnStartup(StartupEventArgs e)
      {
        InitializeAutoMapper();
        base.OnStartup(e);
      }

      private void InitializeAutoMapper()
      {
        Mapper.CreateMap<SubtitleSearchResult, MovieData>();
        Mapper.AssertConfigurationIsValid();
      }
    }
}
