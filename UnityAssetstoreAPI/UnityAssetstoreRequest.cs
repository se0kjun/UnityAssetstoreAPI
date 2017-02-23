using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net;
using System.IO;

namespace UnityAssetstoreAPI
{
    class UnityAssetstoreRequest
    {
        public static string Domain = "https://www.assetstore.unity3d.com/";
        public static string UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/56.0.2924.87 Safari/537.36";
        public static string KharmaVersion = "5.5.0-r88049";
        public static string RequestedWith = "UnityAssetStore";
        public static string UnitySession = "";
        public static CookieContainer cookieContainerAssetstore = new CookieContainer();

        private static string BaseGetResponse(string url, string cookie, string method, string data)
        {
            HttpWebRequest requestAssetstore;
            HttpWebResponse responseAssetstore;
            string resResult;

            Uri uri = new Uri(url); // string 을 Uri 로 형변환
            requestAssetstore = (HttpWebRequest)WebRequest.Create(uri); // WebRequest 객체 형성 및 HttpWebRequest 로 형변환
            requestAssetstore.Method = method; // 전송 방법 "GET" or "POST"
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

        public static T GetResponseToJson<T>(string url, string cookie, string method="GET", string data="")
        {
            try
            {
                string result = BaseGetResponse(url, cookie, method, data);
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
                }

                return default(T);
            }
        }

        public static string GetResponseToString(string url, string cookie, string method = "GET", string data = "")
        {
            try
            {
                string result = BaseGetResponse(url, cookie, method, data);
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
                }

                return null;
            }
        }
    }
}
