﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace VideoRenamer
{
  /// <summary>
  /// Interaction logic for MainWindow.xaml
  /// </summary>
  public partial class MainWindow : Window
  {
    public MainWindow()
    {
      InitializeComponent();
      lstEvents.Items.Add(new Progress { Message = "Hi" });
    }

    private async void Button_Click(object sender, RoutedEventArgs e)
    {
      btnExecute.IsEnabled = false;
      var progress = new Progress<Progress>(prg => lstEvents.Items.Add(prg));
      await Renamer.RenameFilesInDirAsync(@"\\AB\Public\Shared Videos\", x => x.MovieName, progress);
      btnExecute.IsEnabled = true;
    }
  }
}
//