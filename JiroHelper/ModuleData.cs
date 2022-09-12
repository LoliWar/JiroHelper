using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace JiroHelper
{
    public class ModuleData
    {
        public string regexFBDTSG(string src)
        {
            return Regex.Match(src, "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].Value;
        }
        public string regexJaz(string src)
        {
            return Regex.Match(src, "name=\"jazoest\" value=\"(.*?)\"").Groups[1].Value;
        }
    }
}
