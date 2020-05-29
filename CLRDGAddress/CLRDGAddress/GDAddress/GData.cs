using System;
using Newtonsoft.Json;

namespace CLRDGAddress.Abstractions.GDAddress
{
    internal static class GDA
    {
        internal static string REST_URL = "https://chromium-i18n.appspot.com/";
        internal static string REST_ROUTE = "ssl-address/data/";
    }
    public partial class GData
    {
        [JsonProperty("sub_names")]
        public string SubNames { get; set; }

        [JsonProperty("sub_keys")]
        public string SubKeys { get; set; }

        [JsonProperty("sub_isoids")]
        public string SubIsoids { get; set; }

        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("languages")]
        public string Languages { get; set; }

        [JsonProperty("posturl")]
        public Uri Posturl { get; set; }

        [JsonProperty("zipex")]
        public string Zipex { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("zip")]
        public string Zip { get; set; }

        [JsonProperty("upper")]
        public string Upper { get; set; }

        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("fmt")]
        public string Fmt { get; set; }

        [JsonProperty("state_name_type")]
        public string StateNameType { get; set; }

        [JsonProperty("require")]
        public string Require { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }
    }
}
