using System;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace CLRDGAddress.Abstractions.GDAddress
{
    internal static class GDA
    {
        internal static string REST_URL = "https://chromium-i18n.appspot.com/";
        internal static string REST_ROUTE = "ssl-address/data/";
    }
    public partial class GData
    {
        [JsonPropertyName("sub_names")]
        public string SubNames { get; set; }

        [JsonPropertyName("sub_keys")]
        public string SubKeys { get; set; }

        [JsonPropertyName("sub_isoids")]
        public string SubIsoids { get; set; }

        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("languages")]
        public string Languages { get; set; }

        [JsonPropertyName("posturl")]
        public Uri Posturl { get; set; }

        [JsonPropertyName("zipex")]
        public string Zipex { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("zip")]
        public string Zip { get; set; }

        [JsonPropertyName("upper")]
        public string Upper { get; set; }

        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("fmt")]
        public string Fmt { get; set; }

        [JsonPropertyName("state_name_type")]
        public string StateNameType { get; set; }

        [JsonPropertyName("require")]
        public string Require { get; set; }

        [JsonPropertyName("lang")]
        public string Lang { get; set; }
    }
}
