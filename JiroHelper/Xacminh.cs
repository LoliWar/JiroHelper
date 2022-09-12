using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace JiroHelper
{
    public class Xacminh
    {
        public static string Authencation(string key)
        {
            try
            {
                key = key.Replace("\r", "").Replace("\n", "").Replace(" ", "").ToString();
                WebClient webclient = new WebClient();
                string response = webclient.DownloadString("http://2fa.live/tok/" + key);
                return Regex.Match(response, "token\":\"(\\d+)\"").Groups[1].Value;
            }
            catch
            {
                return "error";
            }
        }
    }
}
