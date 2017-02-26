using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityAssetstoreAPI.wrapper;
using System.Net;

namespace UnityAssetstoreAPI
{
    class UnityAssetstoreAsset
    {
        private const string ASSET_USER_OVERVIEW_URL = "https://www.assetstore.unity3d.com/api/ko-KR/content/user-overview/{0}.json";
        private const string ASSET_OVERVIEW_URL = "https://www.assetstore.unity3d.com/api/ko-KR/content/overview/{0}.json";
        
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
        }
    }
}
