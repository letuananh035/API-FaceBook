using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;



namespace UserFB
{
    public static class React
    {
        public const string LIKE = "LIKE";
        public const string NONE = "NONE";
        public const string ANGRY = "ANGRY";
        public const string LOVE = "LOVE";
        public const string WOW = "WOW";
        public const string HAHA = "HAHA";
        public const string SAD = "SAD";
        public const string THANKFUL = "THANKFUL";
    }
    public static class Script 
    {
        public static string MakeTags(string List)
        {
            if (List == "") return "";
            string[] arr = List.Split(',');
            string result = "";
            for (int i = 0; i < arr.Length; ++i)
            {
                result += "@[" + arr[i] + ":0] ";
            }
            return result;
        }
    }
    public class FBAPI
    {
        private string ApiKey = "882a8490361da98702bf97a021ddc14d";
        private string SecretKey = "62f8ce9f74b12f84c123cc23437a4a32";
        private string access_token = "";
        public FBAPI(string user, string pass)
        {
            access_token = GetAccessToken(user, pass);
            //if (Properties.Settings.Default.access_token == "")
            //{
            //    Properties.Settings.Default.access_token = GetAccessToken(user, pass);
            //    Properties.Settings.Default.Save();
            //    access_token = Properties.Settings.Default.access_token;
            //}
            //else
            //{
            //    Properties.Settings.Default.access_token = GetAccessToken(user, pass);
            //    Properties.Settings.Default.Save();
            //    access_token = Properties.Settings.Default.access_token;
            //}
        }

        private string CreateSig(string user, string pass)
        {
            string key = "";
            key += "api_key=" + this.ApiKey;
            key += "email=" + user;
            key += "format=JSON";
            key += "locale=vi_vn";
            key += "method=auth.login";
            key += "password=" + pass;
            key += "return_ssl_resources=0";
            key += "v=1.0";
            key += this.SecretKey;
            /*MD5*/
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(key));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            /*MD5*/
            return hash.ToString();
        }

        public string GetAccessToken(string user, string pass)
        {
            string key = "";
            key += "api_key=" + this.ApiKey;
            key += "&email=" + user;
            key += "&format=JSON";
            key += "&locale=vi_vn";
            key += "&method=auth.login";
            key += "&password=" + pass;
            key += "&return_ssl_resources=0";
            key += "&v=1.0";
            key += "&sig=" + CreateSig(user, pass);
            string url = "https://api.facebook.com/restserver.php?" + key;
            string result = new WEBAPI(url).GET();
            dynamic data = JsonConvert.DeserializeObject(result);
            return data["access_token"];
        }

        public string GETStatus(string id, string limit = "10", string offset = "0")
        {
            string url = "https://graph.facebook.com/" + id + "/feed?fields=id,message,description,created_time,from,status_type,comments,likes,type,link,object_id,full_picture,source&access_token=" + this.access_token + "&offset=" + offset + "&limit=" + limit;
            string data = new WEBAPI(url).GET();
            return data;
        }
        public string GETHome(string limit = "10", string offset = "0")
        {
            string url = "https://graph.facebook.com/me/home?fields=id,description,message,created_time,from,comments,status_type,likes,type,link,object_id,full_picture,source&access_token=" + this.access_token + "&offset=" + offset + "&limit=" + limit;
            string data = new WEBAPI(url).GET();
            return data;
        }
        public string GETObject(string id)
        {
            string url = "https://graph.facebook.com/" + id + "?&access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }
        public string DELETEObject(string id)
        {
            string url = "https://graph.facebook.com/" + id + "?&access_token=" + this.access_token;
            string data = new WEBAPI(url).DELETE();
            return data;
        }
        public string GETAvatar(string id)
        {
            string url = "https://graph.facebook.com/" + id + "/picture?type=large&redirect=false&access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }

        public string CheckToken()
        {
            string url = "https://graph.facebook.com/me/permissions?access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }

        public string POSTReactionsPost(string id, string type = React.LIKE)
        {
            string url = "https://graph.facebook.com/" + id + "/reactions?type=" + type + "&access_token=" + this.access_token;
            string data = new WEBAPI(url).POST();
            return data;
        }

        public string GETReactionsPost(string id, string type = React.LIKE)
        {
            string url = "https://graph.facebook.com/" + id + "/reactions?summary=total_count&type=" + type + "&access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }

        public string POSTComment(string id, string text = "", string tags = "")
        {
            string url = "https://graph.facebook.com/" + id + "/comments?message=" + System.Web.HttpUtility.UrlEncode(text) + " " + Script.MakeTags(tags) + "&access_token=" + this.access_token;
            string data = new WEBAPI(url).POST();
            return data;
        }
        public string UPDATEComment(string id, string text = "")
        {
            string url = "https://graph.facebook.com/" + id + "?message=" + text + "&access_token=" + this.access_token;
            string data = new WEBAPI(url).POST();
            return data;
        }
        public string GETNotifications()
        {
            string url = "https://graph.facebook.com/me?fields=notifications.limit(10).include_read(true){id,title,link,unread}" + "&access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }
        public string GETFriends(string id)
        {
            string url = "https://graph.facebook.com/" + id + "/friends?access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }
        public string GETPhotos(string id)
        {
            string url = "https://graph.facebook.com/" + id + "/photos?access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }
        public string GETInbox(string id = "me")
        {

            //string url = "https://graph.facebook.com/me/inbox?access_token=" + this.access_token;
            //string url = "https://graph.facebook.com/fql?q=SELECT viewer_id,recipients,message_count FROM thread WHERE folder_id = 0 ORDER BY message_count DESC&access_token=" + this.access_token;
            //string url = "https://graph.facebook.com/fql?q=select viewer_id,thread_id,message_count from thread where viewer_id = me() and folder_id = 0&access_token=" + this.access_token;
            string url = "https://graph.facebook.com/fql?q=select updated_time,snippet_author,snippet,recipients,viewer_id,thread_id,message_count from thread where folder_id = 0&access_token=" + this.access_token;
            
            string data = new WEBAPI(url).GET();
            return data;
        }
        public string POSTStatus(string id = "me",string message = "",string tags = "")
        {

            string url = "https://graph.facebook.com/" + id + "/feed?message=" + System.Web.HttpUtility.UrlEncode(message) + " " + Script.MakeTags(tags) + "&access_token=" + this.access_token;
            string data = new WEBAPI(url).POST();
            return data;
        }
        public string GETUser()
        {
            string url = "https://graph.facebook.com/me?fields=cover,age_range,name,gender,birthday&access_token=" + this.access_token;
            string data = new WEBAPI(url).GET();
            return data;
        }
    }


}
