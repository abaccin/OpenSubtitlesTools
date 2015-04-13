using System;

namespace OpenSubtitlesHandler
{
  public class DebugEventArgs : EventArgs
  {
    public DebugCode Code { get; private set; }

    public string Text { get; private set; }

    public DebugEventArgs(string text, DebugCode code)
    {
      this.Text = text;
      this.Code = code;
    }
  }
}
