using System;
using System.Collections.Generic;
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

        private string ASSET_DOWNLOAD_PATH = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Unity", "Asset Store-5.x");
        private AssetstoreDownloadInfoWrapper downloadInfo;

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

        public async Task<byte[]> GetDownloadAssetTaskAsync(int id)
        {
            JObject assets = JObject.FromObject(UnityAssetstoreRequest.GetResponseToJson
                (string.Format(ASSET_DOWNLOAD_URL, id), "*/*", "", "", "GET", ""));

            downloadInfo = assets["download"].ToObject<AssetstoreDownloadInfoWrapper>();

            using (WebClient wc = new WebClient())
            {
                wc.DownloadDataCompleted += DownloadDataCompleted;
                return await wc.DownloadDataTaskAsync(new Uri(downloadInfo.URL));
            }
        }

        public AssetstoreDownloadInfoWrapper GetDownloadAssetAsync(int id)
        {
            JObject assets = JObject.FromObject(UnityAssetstoreRequest.GetResponseToJson
                (string.Format(ASSET_DOWNLOAD_URL, id), "*/*", "", "", "GET", ""));

            downloadInfo = assets["download"].ToObject<AssetstoreDownloadInfoWrapper>();

            using (WebClient wc = new WebClient())
            {
                wc.DownloadDataAsync(new Uri(downloadInfo.URL));
                wc.DownloadDataCompleted += DownloadDataCompleted;
            }

            return downloadInfo;
        }

        private void DownloadDataCompleted(object sender, DownloadDataCompletedEventArgs e)
        {
            if (!Directory.Exists(Path.Combine(ASSET_DOWNLOAD_PATH, downloadInfo.PublisherName, downloadInfo.CategoryName)))
            {
                Directory.CreateDirectory(Path.Combine(ASSET_DOWNLOAD_PATH, downloadInfo.PublisherName, downloadInfo.CategoryName));
            }

            Decrypt(e.Result);
            Console.WriteLine("DONE");
        }

        public bool CheckUpdate(int id)
        {

            return false;
        }

        private void Decrypt(byte[] data)
        {
            byte[] src = ToByteArray(downloadInfo.Key);

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
                       fileStream = File.Create(Path.Combine(ASSET_DOWNLOAD_PATH, downloadInfo.PublisherName, downloadInfo.CategoryName, downloadInfo.FileName + ".unitypackage")))
                {
                    decrypted.CopyTo(fileStream);
                }
            }
        }

        public byte[] ToByteArray(string HexString)
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
