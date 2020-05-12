using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;

namespace CLRDGAddress.GDAddress
{
    public static class GDA
    {
        internal static string REST_URL = "https://chromium-i18n.appspot.com/";
        internal static string REST_ROUTE = "ssl-address/data/";
    }
    [DataContract]
    public partial class GData
    {
        [DataMember(Name = "sub_names")]
        public string SubNames { get; set; }

        [DataMember(Name = "sub_keys")]
        public string SubKeys { get; set; }

        [DataMember(Name = "sub_isoids")]
        public string SubIsoids { get; set; }

        [DataMember(Name = "key")]
        public string Key { get; set; }

        [DataMember(Name = "languages")]
        public string Languages { get; set; }

        [DataMember(Name = "posturl")]
        public Uri Posturl { get; set; }

        [DataMember(Name = "zipex")]
        public string Zipex { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "zip")]
        public string Zip { get; set; }

        [DataMember(Name = "upper")]
        public string Upper { get; set; }

        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "fmt")]
        public string Fmt { get; set; }

        [DataMember(Name = "state_name_type")]
        public string StateNameType { get; set; }

        [DataMember(Name = "require")]
        public string Require { get; set; }

        [DataMember(Name = "lang")]
        public string Lang { get; set; }
    }
}
