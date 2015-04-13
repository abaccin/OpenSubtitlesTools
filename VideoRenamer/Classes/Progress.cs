using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoRenamer
{
  class Progress
  {
    public string OldFileName { get; set; }
    public string NewFileName { get; set; }
    public string Message { get; set; }

    public static Progress New()
    {
      return new Progress();
    }

    public Progress SetOldFileName(string oldFileName)
    {
      this.OldFileName = oldFileName;
      return this;
    }

    public Progress SetNewFileName(string newFileName)
    {
      this.NewFileName = newFileName;
      return this;
    }
    public Progress SetMessage(string message)
    {
      this.Message = message;
      return this;
    }

  }
}
