using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using UnityAssetstoreAPI.wrapper;

namespace UnityAssetstoreAPI
{
    class Program
    {
        public static int bbb = 0;
        static void Main(string[] args)
        {
            UnityAssetstoreUser a = new UnityAssetstoreUser();
            AssetstoreUserWrapper b = a.UserLogin("", "");
            UnityAssetstoreAsset aa = new UnityAssetstoreAsset();
            //aa.GetDownloadAsset();

            //Console.WriteLine(b.XUnitySession);

            a.GetDownloadableAssets();
            Console.WriteLine(a.GetUserOverview(a.UserObject.ID));
        }
    }
}
