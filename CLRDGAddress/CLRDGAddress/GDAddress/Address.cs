using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CLRDGAddress.GDAddress
{
    public class Address : GData
    {
        public enum AddressField {
            /// <summary>
            /// N: Name
            /// </summary>
            Name,
            /// <summary>
            /// O: Organisation
            /// </summary>
            Organisation,
            /// <summary>
            /// A: Street Address Line(s)
            /// </summary>
            StreetAddressLines,
            /// <summary>
            /// D: Dependent locality (may be an inner-city district or a suburb)
            /// </summary>
            DependentLocality,
            /// <summary>
            /// C: City or Locality
            /// </summary>
            City,
            /// <summary>
            /// S: Administrative area such as a state, province, island etc
            /// </summary>
            AdministrativeArea,
            /// <summary>
            /// Z: Zip or postal code
            /// </summary>
            PostalCode,
            /// <summary>
            /// X: Sorting code
            /// </summary>
            SortingCode,
            /// <summary>
            /// Unknow field
            /// </summary>
            Unknow

        }

        private static Dictionary<char, AddressField> AddressFieldsDictionary { get; set; } = new Dictionary<char, AddressField>() {
            { 'N',AddressField.Name },
            { 'O',AddressField.Organisation },
            { 'A',AddressField.StreetAddressLines },
            { 'D',AddressField.DependentLocality },
            { 'C',AddressField.City },
            { 'S',AddressField.AdministrativeArea },
            { 'Z',AddressField.PostalCode },
            { 'X',AddressField.SortingCode },
            { 'U',AddressField.Unknow }
        };

        public List<AddressField> GetAddressFields()
        {
            var AddressFields = new List<AddressField>();
            foreach (var item in Require ?? Fmt)
            {
                if (AddressFieldsDictionary.TryGetValue(item, out AddressField addressField))
                {
                    AddressFields.Add(addressField);
                }
            }
            return AddressFields;
        }

        public IEnumerable<string> GetSubRegionsNames()
        {
            return (SubNames ?? SubKeys ?? string.Empty).Split('~').AsEnumerable();
        }

        public IEnumerable<string> GetSubRegionsKeys()
        {
            return (SubKeys ?? string.Empty).Split('~').AsEnumerable();
        }

        public IEnumerable<string> GetAllSubRegionIsoIds()
        {
            return (SubIsoids ?? string.Empty).Split('~').AsEnumerable();
        }

        public async Task<string> GetSubRegionIsoIds(string sub_key)
        {
            string isoID = string.Empty;
            if (HasSubDivisionsIsoId)
            {
                var isoids = this.GetAllSubRegionIsoIds().ToArray();
                var index = GetSubRegionsKeys().ToList().IndexOf(sub_key);
                isoID = isoids[index];
            }
            else
            {
                isoID = await AddressData.GetSubDivisionIsoID(this.Key, sub_key);
            }
            return isoID;
        }

        public bool HasSubDivisionsIsoId
        {
            get
            {
                return GetAllSubRegionIsoIds().Any();
            }
        }

        public IEnumerable<KeyValuePair<string,string>> GetSubRegionsKeyName()
        {
            var keys = GetSubRegionsKeys().ToArray();
            var names = GetSubRegionsNames().ToArray();
            if (keys.Any() && names.Any())
            {
                List<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
                for (int i = 0; i < keys.Count(); i++)
                {
                    list.Add(new KeyValuePair<string,string>(keys[i], names[i]));
                }
                return list;
            }
            return null;
        }
    }
}
