using System;
using System.Threading.Tasks;
using UnityAssetstoreAPI.wrapper;

namespace UnityAssetstoreAPI
{
    class Program
    {
        static void Main(string[] args)
        {
            UnityAssetstoreUser a = new UnityAssetstoreUser();
            AssetstoreUserWrapper b = a.UserLogin("", "");
            UnityAssetstoreAsset aa = new UnityAssetstoreAsset();
            var getListTask = aa.GetDownloadAssetTaskAsync(79854);
            Task.WaitAll(getListTask);

            //Console.WriteLine(b.XUnitySession);

            a.GetDownloadableAssets();
            Console.WriteLine(a.GetUserOverview(a.UserObject.ID));
        }
    }
}
