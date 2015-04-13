using System;

namespace OpenSubtitlesHandler
{
  public class OSHConsole
  {
    public static event EventHandler<DebugEventArgs> LineWritten;

    public static event EventHandler<DebugEventArgs> UpdateLastLine;

    public static void WriteLine(string text, DebugCode code = DebugCode.None)
    {
      if (OSHConsole.LineWritten == null)
        return;
      OSHConsole.LineWritten((object) null, new DebugEventArgs(text, code));
    }

    public static void UpdateLine(string text, DebugCode code = DebugCode.None)
    {
      if (OSHConsole.UpdateLastLine == null)
        return;
      OSHConsole.UpdateLastLine((object) null, new DebugEventArgs(text, code));
    }
  }
}
