using Newtonsoft.Json;

namespace CLRDGAddress.Abstractions.GDAddress
{
    public class SubDivision
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }

        [JsonProperty("lang")]
        public string Lang { get; set; }

        [JsonProperty("isoid")]
        public string IsoId { get; set; }
        [JsonProperty("sub_keys")]
        public string SubKeys { get; set; }
        [JsonProperty("zip")]
        public string Zip { get; set; }
        [JsonProperty("zipex")]
        public string Zipex { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
