using System.Text.Json.Serialization;

namespace CLRDGAddress.Abstractions.GDAddress
{
    public class SubDivision
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }
        [JsonPropertyName("key")]
        public string Key { get; set; }

        [JsonPropertyName("lang")]
        public string Lang { get; set; }

        [JsonPropertyName("isoid")]
        public string IsoId { get; set; }
        [JsonPropertyName("sub_keys")]
        public string SubKeys { get; set; }
        [JsonPropertyName("zip")]
        public string Zip { get; set; }
        [JsonPropertyName("zipex")]
        public string Zipex { get; set; }
        [JsonPropertyName("name")]
        public string Name { get; set; }
    }
}
