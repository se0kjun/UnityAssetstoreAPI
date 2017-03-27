using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityAssetstoreAPI.wrapper
{
    class AssetstoreDownloadInfoWrapper
    {
        [JsonProperty("link")]
        public AssetstoreLinkWrapper Link;
        [JsonProperty("progress")]
        public int Progress;
        [JsonProperty("filename_safe_package_name")]
        public string FileName;
        [JsonProperty("key")]
        public string Key;
        [JsonProperty("filename_safe_category_name")]
        public string CategoryName;
        [JsonProperty("url")]
        public string URL;
        [JsonProperty("filename_safe_publisher_name")]
        public string PublisherName;
        [JsonProperty("id")]
        public string ID;
    }

    class AssetstoreLinkWrapper
    {
        [JsonProperty("id")]
        public string ID;
        [JsonProperty("type")]
        public string Type;
    }
}
