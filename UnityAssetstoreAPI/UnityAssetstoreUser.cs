using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace UnityAssetstoreAPI
{
    class UnityAssetstoreUser
    {
        private const string USER_LOGIN_URL = "";

        public UnityAssetstoreUser()
        {
        }

        public UnityAssetstoreUser(string id, string password)
        {
        }

        public void UserLogin(string id, string password)
        {
            string loginParameter = string.Format("license_hash=&hardware_hash=&language_code=kr&current_package_id=&user=%s&pass=%s", HttpUtility.UrlEncode(id), HttpUtility.UrlEncode(password));
            UnityAssetstoreRequest.GetResponseToJson<USER>(USER_LOGIN_URL, cookie, "POST", loginParameter);
        }
    }
}
