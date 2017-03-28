using Newtonsoft.Json;

namespace UnityAssetstoreAPI.wrapper
{
    public class AssetstoreUserWrapper
    {
        [JsonProperty("country", Required = Required.AllowNull)]
        public string Country { get; set; }
        [JsonProperty("currency", Required = Required.AllowNull)]
        public string Currency { get; set; }
        [JsonProperty("id", Required = Required.Always)]
        public string ID { get; set; }
        [JsonProperty("is_anonymous", Required = Required.Always)]
        public bool Anonymous { get; set; }
        [JsonProperty("kharma_version", Required = Required.Always)]
        public string KharmaVersion { get; set; }
        [JsonProperty("language_code", Required = Required.AllowNull)]
        public string LanguageCode { get; set; }
        [JsonProperty("language_url_code", Required = Required.AllowNull)]
        public string LanguageUrlCode { get; set; }
        [JsonProperty("name", Required = Required.Always)]
        public string Name { get; set; }
        [JsonProperty("nps", Required = Required.Always)]
        public string NPS { get; set; }
        [JsonProperty("publisher", Required = Required.AllowNull)]
        public string Publisher { get; set; }
        [JsonProperty("rounding", Required = Required.AllowNull)]
        public string Rounding { get; set; }
        [JsonProperty("show_intro", Required = Required.AllowNull)]
        public bool ShowIntro { get; set; }
        [JsonProperty("unity_version", Required = Required.AllowNull)]
        public string UnityVersion { get; set; }
        [JsonProperty("username", Required = Required.Always)]
        public string UserName { get; set; }
        [JsonProperty("uuid", Required = Required.Always)]
        public string UUID { get; set; }
        [JsonProperty("vat_percent", Required = Required.AllowNull)]
        public int VatPercent { get; set; }
        [JsonProperty("xunitysession", Required = Required.Always)]
        public string XUnitySession { get; set; }
    }
}
