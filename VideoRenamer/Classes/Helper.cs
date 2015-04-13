using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace VideoRenamer
{
    internal static class Helper
    {

        public static bool IsConnectedToInternet()
        {
            try
            {
                System.Net.Sockets.TcpClient clnt = new System.Net.Sockets.TcpClient("www.google.com", 80);
                clnt.Close();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
