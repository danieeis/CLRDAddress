using System;
using System.Linq;
using Xunit;

namespace CLRDGAddressTest
{
    public class CLRDTest
    {
        [Fact]
        public void GetCountryByCultureTest()
        {
            var countryVElower = CLRDGAddress.CLRD.Countries.CountryByCulture("ve", "en");

            var countryVEAR = CLRDGAddress.CLRD.Countries.CountryByCulture("VE", "ar");

            var countryVEFR = CLRDGAddress.CLRD.Countries.CountryByCulture("VE", "fr-FR");


            Assert.Throws<System.Globalization.CultureNotFoundException>(() => CLRDGAddress.CLRD.Countries.CountryByCulture("VE", "ga"));
            Assert.Throws<ArgumentException>(() => CLRDGAddress.CLRD.Countries.CountryByCulture("", ""));
            Assert.Equal("Venezuela", countryVElower);
            Assert.Equal("فنزويلا", countryVEAR);
            Assert.Equal("Venezuela", countryVEFR);
        }

        [Fact]
        public void GetCountryByLanguageTest()
        {
            Assert.Throws<ArgumentException>(() => CLRDGAddress.CLRD.Countries.CountryByLanguage("", "ga"));
            Assert.Throws<System.Globalization.CultureNotFoundException>(() => CLRDGAddress.CLRD.Countries.CountryByLanguage("VE", "ve"));

            Assert.Throws<ArgumentException>(() => CLRDGAddress.CLRD.Countries.CountryByLanguage("", ""));

            var countryVElower = CLRDGAddress.CLRD.Countries.CountryByLanguage("ve", "ES");

            var countryVEAR = CLRDGAddress.CLRD.Countries.CountryByLanguage("VE", "ar");

            var countryVEFR = CLRDGAddress.CLRD.Countries.CountryByLanguage("VE", "FR");

            Assert.Equal("Venezuela", countryVElower);

            Assert.Equal("فنزويلا", countryVEAR);
            Assert.Equal("Venezuela", countryVEFR);
        }
        [Fact]
        public void GetCountriesByLanguageTest()
        {
            Assert.Throws<System.Globalization.CultureNotFoundException>(() => CLRDGAddress.CLRD.Countries.CountriesByLanguage("GA", "ga", "VE", "AF"));
            var countriesParamsNotFound = CLRDGAddress.CLRD.Countries.CountriesByLanguage("es", "WE", "VE", "AF","asd");
            var countriesParamsExact = CLRDGAddress.CLRD.Countries.CountriesByLanguage("es", "CO", "VE", "AF", "US");


            var countryVElower = CLRDGAddress.CLRD.Countries.CountriesByLanguage("es", "ve");

            var countries = CLRDGAddress.CLRD.Countries.CountriesByLanguage("en");

            var countryVEFR = CLRDGAddress.CLRD.Countries.CountriesByLanguage("it", "FR");

            Assert.Equal(2, countriesParamsNotFound.Length);

            Assert.Equal(4, countriesParamsExact.Length);
            Assert.Throws<ArgumentException>(() => CLRDGAddress.CLRD.Countries.CountriesByLanguage("", ""));

            Assert.NotEmpty(countryVElower);

            Assert.NotEmpty(countries);
            Assert.NotEmpty(countryVEFR);
        }
    }
}
