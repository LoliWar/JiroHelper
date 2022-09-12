using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace JiroHelper
{
    public class PrimeSMS
    {
        public bool Phone(string api,int checkNull,bool isStop = false)
        {
            int dem = 0;
            while(true)
            {
                if (isStop)
                    return false;
                Request request = new Request();
                string resPhone = request.RequestGet($"http://primeotp.me/api.php?apikey={api}&action=create-request&serviceId=1&count=1");
                if(resPhone == null)
                {
                    dem++;
                    if (dem == checkNull)
                        return false;
                }
                GetPhoneRespone.Phone = Regex.Match(resPhone, "sdt\":\"(.*?)\"").Groups[1].Value;
                GetPhoneRespone.requestID = Regex.Match(resPhone, "requestId\":(.*?)}").Groups[1].Value;
                if (GetPhoneRespone.Phone != "" && GetPhoneRespone.requestID != "")
                    return true;
                else
                    Thread.Sleep(TimeSpan.FromSeconds(3));
            }
        }
        //public bool OTP(string api,int count,string requestID)
        //{
        //    for(int i =1;i<=count; i++)
        //    {
        //        Request request = new Request();
        //        string resCode = request.RequestGet($"http://primeotp.me/api.php?apikey={api}&action=data-request&requestId={requestID}");

        //    }
        //}
    }
}
