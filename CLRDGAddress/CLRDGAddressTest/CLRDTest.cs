using System;
using System.Linq;
using System.Text.RegularExpressions;
using Xunit;

namespace CLRDGAddressTest
{
    public class CLRDTest
    {
        [Fact]
        public void GetCountryByCultureTest()
        {
            var countries = CLRDGAddress.Countries.CountriesByLanguage("es", "VE","SD","af","ERD");

            var countriesLang = CLRDGAddress.Countries.CountriesByLanguage("ES");


            Assert.Throws<System.Globalization.CultureNotFoundException>(() => CLRDGAddress.Countries.CountriesByLanguage("VE", "ga"));
            Assert.Throws<ArgumentException>(() => CLRDGAddress.Countries.CountriesByLanguage("", ""));
            Assert.NotEmpty(countries);
            Assert.NotEmpty(countriesLang);
            Assert.Collection(countries, c => { Assert.Equal("VE", c.CountryCode); Assert.Equal("Venezuela", c.CountryName); },
                                         c => Assert.Equal("SD", c.CountryCode),
                                         c => Assert.Equal("AF", c.CountryCode));

            Assert.NotNull(countriesLang.FirstOrDefault(d => d.CountryCode == "VE"));
        }

        [Fact]
        public void GetCountryByLanguageTest()
        {
            Assert.Throws<ArgumentException>(() => CLRDGAddress.Countries.CountryByLanguage("", "ga"));
            Assert.Throws<System.Globalization.CultureNotFoundException>(() => CLRDGAddress.Countries.CountryByLanguage("VE", "ve"));

            Assert.Throws<ArgumentException>(() => CLRDGAddress.Countries.CountryByLanguage("", ""));

            var countryVElower = CLRDGAddress.Countries.CountryByLanguage("ve", "ES");

            var countryVEAR = CLRDGAddress.Countries.CountryByLanguage("VE", "ar");

            var countryVEFR = CLRDGAddress.Countries.CountryByLanguage("VE", "FR");

            Assert.Equal("Venezuela", countryVElower);

            Assert.Equal("فنزويلا", countryVEAR);
            Assert.Equal("Venezuela", countryVEFR);
        }
    }
}
