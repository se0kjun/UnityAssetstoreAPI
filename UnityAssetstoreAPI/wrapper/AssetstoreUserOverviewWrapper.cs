using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnityAssetstoreAPI.wrapper
{
    class AssetstoreUserOverviewWrapper
    {
        [JsonProperty("balance")]
        public AssetstoreUserBalance Balance { get; set; }
        [JsonProperty("bio")]
        public string Bio { get; set; }
        [JsonProperty("editable")]
        public bool Editable { get; set; }
        [JsonProperty("email")]
        public string Email { get; set; }
        [JsonProperty("himself")]
        public bool Himself { get; set; }
        [JsonProperty("id")]
        public string ID { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }

    class AssetstoreUserBalance
    {
        [JsonProperty("amount")]
        public string Amount { get; set; }
        [JsonProperty("amount_text")]
        public string AmountText { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
