using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace JiroHelper
{

    public class GetToken
    {
        public string fb_dtsg { get; set; }
        public string jaz { get; set; }
        public string scope { get; set; }
        public string logger_id { get; set; }
        public string encrypted_post_body { get; set; }

        string url = "https://m.facebook.com/dialog/oauth?client_id=124024574287414&redirect_uri=fbconnect%3A%2F%2Fsuccess&scope=email%2Cpublish_actions%2Cpublish_pages%2Cuser_about_me%2Cuser_actions.books%2Cuser_actions.music%2Cuser_actions.news%2Cuser_actions.video%2Cuser_activities%2Cuser_birthday%2Cuser_education_history%2Cuser_events%2Cuser_games_activity%2Cuser_groups%2Cuser_hometown%2Cuser_interests%2Cuser_likes%2Cuser_location%2Cuser_notes%2Cuser_photos%2Cuser_questions%2Cuser_relationship_details%2Cuser_relationships%2Cuser_religion_politics%2Cuser_status%2Cuser_subscriptions%2Cuser_videos%2Cuser_website%2Cuser_work_history%2Cfriends_about_me%2Cfriends_actions.books%2Cfriends_actions.music%2Cfriends_actions.news%2Cfriends_actions.video%2Cfriends_activities%2Cfriends_birthday%2Cfriends_education_history%2Cfriends_events%2Cfriends_games_activity%2Cfriends_groups%2Cfriends_hometown%2Cfriends_interests%2Cfriends_likes%2Cfriends_location%2Cfriends_notes%2Cfriends_photos%2Cfriends_questions%2Cfriends_relationship_details%2Cfriends_relationships%2Cfriends_religion_politics%2Cfriends_status%2Cfriends_subscriptions%2Cfriends_videos%2Cfriends_website%2Cfriends_work_history%2Cads_management%2Ccreate_event%2Ccreate_note%2Cexport_stream%2Cfriends_online_presence%2Cmanage_friendlists%2Cmanage_notifications%2Cmanage_pages%2Cphoto_upload%2Cpublish_stream%2Cread_friendlists%2Cread_insights%2Cread_mailbox%2Cread_page_mailboxes%2Cread_requests%2Cread_stream%2Crsvp_event%2Cshare_item%2Csms%2Cstatus_update%2Cuser_online_presence%2Cvideo_upload%2Cxmpp_login&response_type=token";
        public string token { get; set; }
        void addParam(ref Request request)
        {
            request.AddHeader("referer", url);
            request.AddParam("fb_dtsg", fb_dtsg);
            request.AddParam("jazoest", jaz);
            request.AddParam("scope", scope);
            request.AddParam("display", "touch");
            request.AddParam("sdk", "");
            request.AddParam("sdk_version", "");
            request.AddParam("domain", "");
            request.AddParam("sso_device", "");
            request.AddParam("state", "");
            request.AddParam("user_code", "");
            request.AddParam("nonce", "");
            request.AddParam("logger_id", logger_id);
            request.AddParam("auth_type", "");
            request.AddParam("auth_nonce", "");
            request.AddParam("code_challenge", "");
            request.AddParam("code_challenge_method", "");
            request.AddParam("encrypted_post_body", encrypted_post_body);
            request.AddParam("return_format[]", "access_token");
        }
        public bool Token(string cookie)
        {
            Request rq = new Request(cookie);
            string resData = rq.RequestGet(url);
            ModuleData moduleData = new ModuleData();
            fb_dtsg = moduleData.regexFBDTSG(resData);
            jaz = moduleData.regexJaz(resData);
            scope = Regex.Match(resData, "name=\"scope\" value=\"(.*?)\"").Groups[1].Value;
            logger_id = Regex.Match(resData, "name=\"logger_id\" value=\"(.*?)\"").Groups[1].Value;
            encrypted_post_body = Regex.Match(resData, "name=\"encrypted_post_body\" value=\"(.*?)\"").Groups[1].Value;
            if (fb_dtsg == "" || jaz == "" || scope == "" || logger_id == "" || encrypted_post_body == "")
                return false;
            addParam(ref rq);
            rq.RequestPost("https://m.facebook.com/dialog/oauth/skip/submit/");
            TokenEAAB(ref rq);
            return true;
        }
        public void TokenEAAB(ref Request request)
        {

            request.RequestGet("https://www.facebook.com/dialog/oauth?client_id=124024574287414&redirect_uri=https://www.instagram.com/accounts/signup/&&scope=email&response_type=token");
            string uri = request.Uri();
            token = Regex.Match(uri, "token=(.*?)&", RegexOptions.Singleline).Groups[1].Value;
        }

    }
}
