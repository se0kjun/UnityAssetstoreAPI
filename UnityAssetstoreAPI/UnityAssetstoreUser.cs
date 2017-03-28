using System.Collections.Generic;
using System.Web;
using UnityAssetstoreAPI.wrapper;
using System.Net;
using Newtonsoft.Json.Linq;

namespace UnityAssetstoreAPI
{
    class UnityAssetstoreUser
    {
        private const string USER_LOGIN_URL = "https://www.assetstore.unity3d.com/login";
        private const string SEARCH_URL = "https://www.assetstore.unity3d.com/api/ko-KR/account/downloads/search.json";
        private const string USER_OVERVIEW_URL = "https://www.assetstore.unity3d.com/api/ko-KR/user/overview/{0}.json";

        private CookieContainer cookie;
        private AssetstoreUserWrapper userAuthenticate;
        public AssetstoreUserWrapper UserObject
        {
            get
            {
                if (userAuthenticate != null) { return userAuthenticate; }
                else
                {
                    // require to login
                    return null;
                }
            }
        }

        public UnityAssetstoreUser()
        {
        }

        public UnityAssetstoreUser(string id, string password)
        {
        }

        public AssetstoreUserWrapper UserLogin(string id, string password)
        {
            string loginParameter = string.Format(
                "license_hash=&hardware_hash=&language_code=kr&current_package_id=&user={0}&pass={1}", 
                HttpUtility.UrlEncode(id), 
                HttpUtility.UrlEncode(password));

            userAuthenticate = UnityAssetstoreRequest.GetResponseToJson<AssetstoreUserWrapper>
                (USER_LOGIN_URL, 
                "application/json", 
                "application/x-www-form-urlencoded; charset=UTF-8", 
                "", "POST", loginParameter);

            UnityAssetstoreRequest.KharmaVersion = userAuthenticate.KharmaVersion;
            UnityAssetstoreRequest.UnitySession = userAuthenticate.XUnitySession;

            return userAuthenticate;
        }

        public List<int> GetDownloadableAssets()
        {
            List<int> assetsId = new List<int>();
            JObject assets =JObject.FromObject( UnityAssetstoreRequest.GetResponseToJson
                (SEARCH_URL, "*/*", "application/x-www-form-urlencoded; charset=UTF-8", "", "POST", ""));
            foreach (JToken asset in assets["results"].Children())
            {
                assetsId.Add(asset["id"].Value<int>());
            }

            return assetsId;
        }

        public AssetstoreUserOverviewWrapper GetUserOverview(string id)
        {
            return UnityAssetstoreRequest.GetResponseToJson<AssetstoreUserOverviewWrapper>
                (string.Format(USER_OVERVIEW_URL, id), "*/*", "", ""); ;
        }
    }
}
