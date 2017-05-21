using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace UserFB
{
    class WEBAPI
    {

        public string url = "";

        public WEBAPI(string link)
        {
            this.url = link;
        }
        public string GET()
        {
            try
            {
                string responseContent;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.Proxy = null;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/61.4.114 Chrome/55.4.2883.114 Safari/537.36";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                            responseContent = sr.ReadToEnd();
                    }
                }
                return responseContent;

            }
            catch (Exception ex)
            {
                // Error handling
                return "";
            }
        }
        public string POST()
        {
            try
            {
                string responseContent;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "POST";
                request.Proxy = null;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/61.4.114 Chrome/55.4.2883.114 Safari/537.36";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                            responseContent = sr.ReadToEnd();
                    }
                }
                return responseContent;

            }
            catch (Exception ex)
            {
                // Error handling
                return "";
            }
        }
        public string DELETE()
        {
            try
            {
                string responseContent;
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "DELETE";
                request.Proxy = null;
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) coc_coc_browser/61.4.114 Chrome/55.4.2883.114 Safari/537.36";
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader sr = new StreamReader(responseStream))
                            responseContent = sr.ReadToEnd();
                    }
                }
                return responseContent;

            }
            catch (Exception ex)
            {
                // Error handling
                return "";
            }
        }
    }
}
