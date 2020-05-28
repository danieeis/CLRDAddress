using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml;
using CLRDGAddress.Abstractions;

namespace CLRDGAddress
{
    public static class Countries
    {
        static readonly string ResourceBasePath = "CLRDGAddress.CLRD.";
        static readonly string ResourceNotFoundMessage = "CLRD source not found";
        static readonly string ArgumentExceptionMessage = "The params can't be null or empty";
        /// <summary>
        /// Country by Language ISO code
        /// </summary>
        /// <param name="code">Country code</param>
        /// <param name="ISOlanguage">ISO language</param>
        /// <returns></returns>
        public static string CountryByLanguage(string code, string ISOlanguage)
        {
            if (string.IsNullOrEmpty(ISOlanguage) || string.IsNullOrEmpty(code))
            {
                throw new ArgumentException(ArgumentExceptionMessage);
            }
            var xdoc = GetEntryXmlDoc(GetResourceFile(ISOlanguage.ToLower() + ".xml"));
            if (xdoc == null) throw new CultureNotFoundException(ResourceNotFoundMessage);
            var xn = xdoc.SelectSingleNode("//territory[@type='" + code.ToUpper() + "']");
            return xn?.InnerText;
        }
        /// <summary>
        /// Get all the countries by iso language
        /// </summary>
        /// <param name="codes">The ISO countries codes</param>
        /// <param name="ISOlanguage">the iso language</param>
        public static List<Country> CountriesByLanguage(string ISOlanguage, params string[] codes)
        {
            if (string.IsNullOrEmpty(ISOlanguage) || !codes.Any())
            {
                throw new ArgumentException(ArgumentExceptionMessage);
            }
            var xdoc = GetEntryXmlDoc(GetResourceFile(ISOlanguage.ToLower() + ".xml"));
            if (xdoc == null) throw new CultureNotFoundException(ResourceNotFoundMessage);
            List<Country> countries = new List<Country>();
            for (int i = 0; i < codes.Length; i++)
            {
                var node = xdoc?.SelectSingleNode("//territory[@type='" + codes[i].ToUpper() + "']");
                if (node != null)
                {

                    countries.Add(new Country() { CountryCode = node.Attributes["type"].Value, CountryName =  node.InnerText });
                }
                else
                {
                    Console.WriteLine($"{DateTime.Now} | {typeof(Countries).Assembly.GetName()} : Country code not found: {codes[i].ToUpper()}");
                }
            }
            return countries;
        }
        /// <summary>
        /// Get all countries code and names
        /// </summary>
        /// <param name="ISOLanguage">iso language code</param>
        /// <returns></returns>
        public static List<Country> CountriesByLanguage(string ISOLanguage)
        {
            if (string.IsNullOrEmpty(ISOLanguage))
            {
                throw new ArgumentException(ArgumentExceptionMessage);
            }
            var xdoc = GetEntryXmlDoc(GetResourceFile(ISOLanguage.ToLower() + ".xml"));
            if (xdoc == null) throw new CultureNotFoundException(ResourceNotFoundMessage);
            System.Xml.XmlNodeList xn = xdoc.GetElementsByTagName("territory");
            List<Country> countries = new List<Country>();
            for (int i = 0; i < xn.Count; i++)
            {
                var code = xn[i].Attributes["type"].Value;
                if (Regex.Match(code, "^[A-Z]{2}$").Success)
                {
                    countries.Add(new Country() { CountryCode = code, CountryName = xn[i].InnerText });
                }
            }
            return countries;
        }

        static byte[] GetResourceFile(string filename)
        {
            using (Stream stream = typeof(Countries).Assembly.
                        GetManifestResourceStream(ResourceBasePath + filename))
            {
                if (stream == null) return null;
                byte[] buffer = new byte[stream.Length];
                stream.Read(buffer, 0, buffer.Length);
                return buffer;
            }
        }

        static XmlDocument GetEntryXmlDoc(byte[] Bytes)
        {
            if (Bytes == null) return null;
            XmlDocument xmlDoc = new XmlDocument();
            using (MemoryStream ms = new MemoryStream(Bytes))
            {
                xmlDoc.Load(ms);
            }
            return xmlDoc;
        }
    }
}
