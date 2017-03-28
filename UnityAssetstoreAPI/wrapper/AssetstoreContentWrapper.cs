using System.Collections.Generic;
using Newtonsoft.Json;

namespace UnityAssetstoreAPI.wrapper
{
    class AssetstoreContentWrapper
    {
        [JsonProperty("category")]
        public AssetstoreContentCategoryWrapper Category;
        [JsonProperty("description")]
        public string Description;
        [JsonProperty("first_published_at")]
        public string FirstPublishedAt;
        [JsonProperty("id")]
        public string ID;
        [JsonProperty("license")]
        public string License;
        [JsonProperty("link")]
        public AssetstoreContentLinkWrapper Link;
        [JsonProperty("min_unity_version")]
        public string MinUnityVersion;
        [JsonProperty("package_version_id")]
        public string PackageVersionID;
        [JsonProperty("price")]
        public AssetstoreContentPriceWrapper Price;
        [JsonProperty("price_original")]
        public AssetstoreContentPriceOriginalWrapper PriceOriginal;
        [JsonProperty("pubdate")]
        public string PubDate;
        [JsonProperty("publishnotes")]
        public string PublishNotes;
        [JsonProperty("rating")]
        public AssetstoreContentRatingWrapper Rating;
        [JsonProperty("short_url")]
        public string ShortURL;
        [JsonProperty("sizetext")]
        public string SizeText;
        [JsonProperty("title")]
        public string Title;
        [JsonProperty("unity_versions")]
        public List<string> UnityVersions;
        [JsonProperty("url")]
        public string URL;
        [JsonProperty("version")]
        public string Version;
    }

    class AssetstoreContentCategoryWrapper
    {
        [JsonProperty("id")]
        public string ID;
        [JsonProperty("label")]
        public string Label;
        [JsonProperty("multiple")]
        public string Multiple;
        [JsonProperty("tree_id")]
        public string TreeId;
    }

    class AssetstoreContentLinkWrapper
    {
        [JsonProperty("id")]
        public string ID;
        [JsonProperty("type")]
        public string Type;
    }

    class AssetstoreContentPriceWrapper
    {
        public string DKK;
        public string EUR;
        public string JPY;
        public string USD;
        [JsonProperty("sale_id")]
        public string SaleID;
    }

    class AssetstoreContentPriceOriginalWrapper
    {
        public string DKK;
        public string EUR;
        public string JPY;
        public string USD;
    }

    class AssetstoreContentRatingWrapper
    {
        [JsonProperty("average")]
        public string Average;
        [JsonProperty("count")]
        public string Count;
    }
}
