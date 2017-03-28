using System;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace UnityAssetstoreAPI
{
    class UnityAssetstoreRequest
    {
        public static string Domain = "https://www.assetstore.unity3d.com";
        public static string UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
        public static string KharmaVersion = "5.5.0-r88051";
        public static string RequestedWith = "UnityAssetStore";
        public static string UnitySession = "";
        public static CookieContainer cookieContainerAssetstore = new CookieContainer();

        private static string BaseGetResponse(string url, string accept, string content_type, string cookie, string method, string data)
        {
            HttpWebRequest requestAssetstore;
            HttpWebResponse responseAssetstore;
            string resResult;

            // init
            if (UnitySession == string.Empty)
            {
                Uri uri_session = new Uri("https://www.assetstore.unity3d.com/login");
                requestAssetstore = (HttpWebRequest)WebRequest.Create(uri_session);
                requestAssetstore.Method = "GET";
                using (responseAssetstore = (HttpWebResponse)requestAssetstore.GetResponse())
                {
                    Stream respPostStream = responseAssetstore.GetResponseStream();
                    StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("EUC-KR"), true);

                    UnityAssetstoreRequest.UnitySession = readerPost.ReadToEnd();
                }
            }

            Uri uri = new Uri(url); 
            requestAssetstore = (HttpWebRequest)WebRequest.Create(uri); 
            requestAssetstore.Method = method;
            requestAssetstore.Accept = accept;
            requestAssetstore.ContentType = content_type;
            requestAssetstore.Host = "www.assetstore.unity3d.com";
            requestAssetstore.UserAgent = UnityAssetstoreRequest.UserAgent;
            requestAssetstore.Headers.Add("X-Kharma-Version", UnityAssetstoreRequest.KharmaVersion);
            requestAssetstore.Headers.Add("X-Requested-With", UnityAssetstoreRequest.RequestedWith);
            requestAssetstore.Headers.Add("X-Unity-Session", UnityAssetstoreRequest.UnitySession);
            requestAssetstore.ServicePoint.Expect100Continue = false;
            requestAssetstore.CookieContainer = new CookieContainer();
            requestAssetstore.CookieContainer.SetCookies(uri, cookie); 

            if (method == "POST")
            {
                byte[] byteArray = Encoding.UTF8.GetBytes(data);
                Stream dataStream = requestAssetstore.GetRequestStream();
                dataStream.Write(byteArray, 0, byteArray.Length);
                dataStream.Close();
            }

            using (responseAssetstore = (HttpWebResponse)requestAssetstore.GetResponse())
            {
                Stream respPostStream = responseAssetstore.GetResponseStream();
                StreamReader readerPost = new StreamReader(respPostStream, Encoding.GetEncoding("EUC-KR"), true);

                resResult = readerPost.ReadToEnd();
            }

            return resResult;
        }

        public static T GetResponseToJson<T>(string url, string accept, string content_type, string cookie, string method="GET", string data="")
        {
            try
            {
                string result = BaseGetResponse(url, accept, content_type, cookie, method, data);
                return JsonConvert.DeserializeObject<T>(result);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                    }
                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                    }
                    else if (resp.StatusCode == HttpStatusCode.Conflict)
                    {
                        // need to update kharma version
                    }
                }

                return default(T);
            }
        }

        public static object GetResponseToJson(string url, string accept, string content_type, string cookie, string method = "GET", string data = "")
        {
            try
            {
                string result = BaseGetResponse(url, accept, content_type, cookie, method, data);
                return JsonConvert.DeserializeObject(result);
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                    }
                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                    }
                    else if (resp.StatusCode == HttpStatusCode.Conflict)
                    {
                        // need to update kharma version
                    }
                }

                return null;
            }
        }

        public static string GetResponseToString(string url, string accept, string content_type, string cookie, string method = "GET", string data = "")
        {
            try
            {
                string result = BaseGetResponse(url, accept, content_type, cookie, method, data);
                return result;
            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError && ex.Response != null)
                {
                    var resp = (HttpWebResponse)ex.Response;
                    if (resp.StatusCode == HttpStatusCode.NotFound)
                    {
                    }
                    else if (resp.StatusCode == HttpStatusCode.Unauthorized)
                    {
                    }
                    else if (resp.StatusCode == HttpStatusCode.Conflict)
                    {
                        // need to update kharma version
                    }
                }

                return null;
            }
        }
    }
}
