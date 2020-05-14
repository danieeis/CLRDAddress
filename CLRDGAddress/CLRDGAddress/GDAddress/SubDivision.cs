using System.Runtime.Serialization;

namespace CLRDGAddress.Abstractions.GDAddress
{
    [DataContract]
    public class SubDivision
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }
        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "lang")]
        public string Lang { get; set; }

        [DataMember(Name = "isoid")]
        public string IsoId { get; set; }
        [DataMember(Name = "sub_keys")]
        public string SubKeys { get; set; }
        [DataMember(Name = "zip")]
        public string Zip { get; set; }
        [DataMember(Name = "zipex")]
        public string Zipex { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
