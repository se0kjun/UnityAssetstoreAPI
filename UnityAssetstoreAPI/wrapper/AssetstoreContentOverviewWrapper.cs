using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityAssetstoreAPI.wrapper
{
    class AssetstoreContentOverviewWrapper
    {
        [JsonProperty("can_download")]
        public bool IsDownload;
        [JsonProperty("can_update")]
        public bool IsUpdate;
        [JsonProperty("express_checkout")]
        public bool ExpressCheckout;
        [JsonProperty("in_users_downloads")]
        public int UsersDownloads;
        [JsonProperty("is_complete_project")]
        public bool IsCompleteProject;
        [JsonProperty("is_on_wishlist")]
        public bool IsOnWishlist;
        [JsonProperty("last_downloaded_at", Required = Required.AllowNull)]
        public string LastDownloadedAt;
        [JsonProperty("purchased_at", Required = Required.AllowNull)]
        public string PurchasedAt;
        [JsonProperty("upgrade_package", Required = Required.AllowNull)]
        public bool UpgradePackage;
    }
}
