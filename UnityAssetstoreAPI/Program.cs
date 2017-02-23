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
        static void Main(string[] args)
        {
            UnityAssetstoreUser a = new UnityAssetstoreUser();
            AssetstoreUserWrapper b = a.UserLogin("", "");
            Console.WriteLine(b.XUnitySession);

            a.GetDownloadableAssets();
        }
    }
}
