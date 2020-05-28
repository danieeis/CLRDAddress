using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CLRDGAddress;

namespace CLRDGAddress.Abstractions.GDAddress
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
            if (!HasAddressFields) return AddressFields;
            foreach (var item in Require ?? Fmt)
            {
                if (AddressFieldsDictionary.TryGetValue(item, out AddressField addressField))
                {
                    AddressFields.Add(addressField);
                }
            }
            return AddressFields;
        }

        public List<AddressField> GetAddressRequiredFields()
        {
            var AddressFields = new List<AddressField>();
            if (string.IsNullOrEmpty(Require)) return AddressFields;
            foreach (var item in Require)
            {
                if (AddressFieldsDictionary.TryGetValue(item, out AddressField addressField))
                {
                    AddressFields.Add(addressField);
                }
            }
            return AddressFields;
        }
        public string GetSubRegionsName(string SubKeyOrIsoID)//D o Maracay
        {
            string name = string.Empty;
            var subisos = GetAllSubRegionIsoIds().ToList();
            if (subisos != null && subisos.IndexOf(SubKeyOrIsoID) != -1)
            {
                var index = subisos.IndexOf(SubKeyOrIsoID);

                var names = GetSubRegionsNames();

                if (names?.Length > index)
                {
                    name = names[index];
                }
            }
            else
            {
                name = GetSubRegionsKeyName().FirstOrDefault(d => d.Key == SubKeyOrIsoID).Value ?? string.Empty;
            }

            return name;
        }
        public string[] GetSubRegionsNames()
        {
            if (string.IsNullOrEmpty(SubNames) && string.IsNullOrEmpty(SubKeys)) return new string[0];
            return (SubNames ?? SubKeys).Split('~');
        }

        public string[] GetSubRegionsKeys()
        {
            if (string.IsNullOrEmpty(SubKeys)) return new string[0];
            return SubKeys.Split('~');
        }

        public string[] GetAllSubRegionIsoIds()
        {
            if (string.IsNullOrEmpty(SubIsoids)) return new string[0];
            return SubIsoids.Split('~');
        }

        public async Task<string> GetSubRegionIsoIds(string sub_key)
        {
            string isoID;
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
        public bool HasAddressFields
        {
            get
            {
                return !string.IsNullOrEmpty(Require) || !string.IsNullOrEmpty(Fmt);
            }
        }

        public Dictionary<string,string> GetSubRegionsKeyName()
        {
            var keys = GetSubRegionsKeys().ToArray();
            var names = GetSubRegionsNames().ToArray();
            if (keys.Any() && names.Any())
            {
                Dictionary<string, string> list = new Dictionary<string, string>();
                for (int i = 0; i < keys.Count(); i++)
                {
                    list.Add(keys[i], names[i]);
                }
                return list;
            }
            return null;
        }
    }
}
