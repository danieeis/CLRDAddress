using Xunit;
using System.Linq;
using System.Net.Http;
using System;

namespace CLRDGAddressTest
{
    public class GAddressTest
    {
        [Fact]
        public async void GetAddressesTest()
        {
            var addresses = await CLRDGAddress.AddressData.GetAddresses("VE");
            
            var fields = addresses.GetAddressFields();
            
            var subkeyname = addresses.GetSubRegionsKeyName();
            Assert.NotEmpty(subkeyname);
            var subNames = addresses.GetSubRegionsNames();
            Assert.NotEmpty(subNames);
            var subkeys = addresses.GetSubRegionsKeys();
            Assert.NotEmpty(subkeys);
            var allisoid = addresses.GetAllSubRegionIsoIds();
            Assert.NotEmpty(allisoid);

            if (subkeyname.ContainsValue("Amazonas"))
            {
                var key = subkeyname.FirstOrDefault(d => d.Value == "Amazonas").Key;
                var subkeyisoid = addresses.GetSubRegionIsoIds(key);
                Assert.NotNull(subkeyisoid);
            }
            Assert.NotEmpty(fields);

            Assert.Equal(3, fields.Count);
            Assert.True(addresses.HasSubDivisionsIsoId);
            Assert.True(addresses.HasAddressFields);
            Assert.NotNull(addresses.Id);

        }

        [Fact]
        public async void GetAddressesWithoutRegionsTest()
        {
            var addresses = await CLRDGAddress.AddressData.GetAddresses("af");
            var addresses2 = await CLRDGAddress.AddressData.GetAddresses("AF");
            var subregions = addresses.GetSubRegionsNames();
            Assert.Empty(subregions);
            var fields = addresses2.GetAddressFields();
            Assert.Empty(addresses2.GetAllSubRegionIsoIds());
            Assert.False(addresses2.HasSubDivisionsIsoId);
            Assert.True(addresses2.HasAddressFields);
            Assert.NotEmpty(fields);
            Assert.NotNull(addresses.Id);
            Assert.NotNull(addresses2.Id);
        }

        [Fact]
        public async void GetAddressesEmpty()
        {
            var addresses = await CLRDGAddress.AddressData.GetAddresses("a");

            var addresses2 = await CLRDGAddress.AddressData.GetAddresses("");

            Assert.Null(addresses.Id);
            Assert.Null(addresses2.Id);

        }

        [Fact]
        public async void GetSubRegionName()
        {
            var addresses = await CLRDGAddress.AddressData.GetAddresses("VE");
            var name = addresses.GetSubRegionsName("E");
            var name2 = addresses.GetSubRegionsName("Maracay");
            var name3 = addresses.GetSubRegionsName("Barinas");

            Assert.Equal("Barinas", name);
            Assert.Empty(name2);
            Assert.Equal("Barinas", name3);
        }
    }
}
