using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace JiroHelper
{
    public class Facebook_Helper
    {
        public static string Recaptcha(string keycaptcha, string datasitekey, string site)
        {
            Request request = new Request();
            string GetID = request.RequestGet("https://2captcha.com/in.php?key=" + keycaptcha + "&method=userrecaptcha&googlekey=" + datasitekey + "&pageurl=" + site).ToString();
            string GetIDC = Regex.Match(GetID, @"\d{1,50}").ToString();
            string Code = "";
            do
            {
                string GetCode_ = request.RequestGet("https://2captcha.com/res.php?key=" + keycaptcha + "&action=get&id=" + GetIDC).ToString();
                try
                {
                    Code = GetCode_.Replace("OK|", "");
                    if (Code == "ERROR_CAPTCHA_UNSOLVABLE")
                    {
                        return "error";
                    }
                }
                catch
                {

                }
                Thread.Sleep(5000);
            }
            while (Code == "CAPCHA_NOT_READY");
            Thread.Sleep(500);
            return Code;
        }

        public string GetMail_Tempmail(string address)
        {
            var client = new RestClient(address);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            var body = @"{
" + "\n" +
 @"    ""cmd"": ""request.post"",
" + "\n" +
 @"    ""url"": ""https://web2.temp-mail.org/mailbox"",
" + "\n" +
 @"    ""maxTimeout"": 10000,
" + "\n" +
 @"    ""postData"": ""application/x-www-form-urlencoded""
" + "\n" +
 @"}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            string unscape = Regex.Unescape(response.Content);
            string mails = Regex.Match(unscape, "mailbox\":\"(.*?)\"").Groups[1].ToString();
            return mails;
        }
    }
}
