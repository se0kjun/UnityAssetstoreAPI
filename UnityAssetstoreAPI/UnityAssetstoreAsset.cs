using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityAssetstoreAPI.wrapper;
using System.Net;
using Newtonsoft.Json.Linq;
using System.IO;
using System.Security.Cryptography;

namespace UnityAssetstoreAPI
{
    class UnityAssetstoreAsset
    {
        private const string ASSET_USER_OVERVIEW_URL = "https://www.assetstore.unity3d.com/api/ko-KR/content/user-overview/{0}.json";
        private const string ASSET_OVERVIEW_URL = "https://www.assetstore.unity3d.com/api/ko-KR/content/overview/{0}.json";
        private const string ASSET_DOWNLOAD_URL = "https://www.assetstore.unity3d.com/api/ko-KR/content/download/{0}.json";

        private string packageKey;

        public UnityAssetstoreAsset()
        {
        }

        public AssetstoreContentOverviewWrapper GetAssetOverview(int id)
        {
            return UnityAssetstoreRequest.GetResponseToJson<AssetstoreContentOverviewWrapper>
                (string.Format(ASSET_USER_OVERVIEW_URL, id), "*/*", "", "");
        }

        public List<AssetstoreContentOverviewWrapper> GetAssetsOverview(List<int> ids)
        {
            List<AssetstoreContentOverviewWrapper> data = new List<AssetstoreContentOverviewWrapper>();

            foreach (int id in ids)
            {
                data.Add(UnityAssetstoreRequest.GetResponseToJson<AssetstoreContentOverviewWrapper>
                    (string.Format(ASSET_USER_OVERVIEW_URL, id), "*/*", "", ""));
            }

            return data;
        }

        public AssetstoreContentWrapper GetAssetInfo (int id)
        {
            return UnityAssetstoreRequest.GetResponseToJson<AssetstoreContentWrapper>
                (string.Format(ASSET_OVERVIEW_URL, id), "*/*", "", "");
        }

        public List<AssetstoreContentWrapper> GetAssetsInfo(List<int> ids)
        {
            List<AssetstoreContentWrapper> data = new List<AssetstoreContentWrapper>();

            foreach (int id in ids)
            {
                data.Add(UnityAssetstoreRequest.GetResponseToJson<AssetstoreContentWrapper>
                    (string.Format(ASSET_USER_OVERVIEW_URL, id), "*/*", "", ""));
            }

            return data;
        }

        public void GetDownloadAsset(int id)
        {
            JObject assets = JObject.FromObject(UnityAssetstoreRequest.GetResponseToJson
                (string.Format(ASSET_DOWNLOAD_URL, id), "*/*", "", "", "GET", ""));

            JToken info = assets["download"];
            packageKey = info.Value<string>("key");

            using (WebClient wc = new WebClient())
            {
                wc.DownloadDataAsync(new Uri(info.Value<string>("url")));
                wc.DownloadDataCompleted += DownloadDataCompleted;
            }
        }

        private void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            Console.WriteLine("COMPLETE");
            Decrypt(e.Result);
            Console.WriteLine("DONE");
        }

        private void Decrypt(byte[] data)
        {
            byte[] src = ToByteArray(packageKey);

            byte[] keyhash = new byte[32];
            Array.Copy(src, 0, keyhash, 0, 32);
            byte[] ivhash = new byte[16];
            Array.Copy(src, 32, ivhash, 0, 16);

            using (var rijndaelManaged = new RijndaelManaged
            {
                Key = keyhash,
                IV = ivhash,
                Mode = CipherMode.CBC,
                Padding = PaddingMode.None,
                BlockSize = 128,
                KeySize = 256
            })
            {
                using (Stream encrypted = new MemoryStream(data),
                       decrypted = new CryptoStream(encrypted, rijndaelManaged.CreateDecryptor(keyhash, ivhash), CryptoStreamMode.Read),
                       copy = new MemoryStream(),
                       fileStream = File.Create("test2.unitypackage"))
                {
                    decrypted.CopyTo(fileStream);
                }
            }
        }

        public byte[] ToByteArray(String HexString)
        {
            int NumberChars = HexString.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
            {
                bytes[i / 2] = Convert.ToByte(HexString.Substring(i, 2), 16);
            }
            return bytes;
        }
    }
}
