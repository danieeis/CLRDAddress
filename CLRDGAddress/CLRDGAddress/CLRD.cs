using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml;

namespace CLRDGAddress
{
    public class CLRD
    {
        static string ResourceBasePath = "CLRDGAddress.CLRD.";
        static string ResourceNotFoundMessage = "CLRD source not found";
        static string ArgumentExceptionMessage = "The params can't be null or empty";
        public class Countries
        {

            /// <summary>
            /// Get the country by Culture specification string "en-US"
            /// </summary>
            /// <param name="code">Country ISO code</param>
            /// <param name="culture">The culture specification "en-US"</param>
            /// <returns></returns>
            public static string CountryByCulture(string code, string culture)
            {
                if (string.IsNullOrEmpty(culture) || string.IsNullOrEmpty(code))
                {
                    throw new ArgumentException(ArgumentExceptionMessage);
                }
                var locale = new System.Globalization.CultureInfo(culture).TwoLetterISOLanguageName.ToLower();
                var xdoc = GetEntryXmlDoc(GetResourceFile(locale + ".xml"));
                if (xdoc == null) throw new CultureNotFoundException(ResourceNotFoundMessage);
                var xn = xdoc.SelectSingleNode($"//territory[@type='{code.ToUpper()}']");
                return xn?.InnerText;
            }
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
            /// Get all the countries by language
            /// </summary>
            /// <param name="ISOlanguage">language iso code</param>
            /// <returns></returns>
            public static string[] CountriesByLanguage(string ISOlanguage)
            {
                if (string.IsNullOrEmpty(ISOlanguage))
                {
                    throw new ArgumentException(ArgumentExceptionMessage, "ISOlanguage");
                }
                var xdoc = GetEntryXmlDoc(GetResourceFile(ISOlanguage.ToLower() + ".xml"));
                if (xdoc == null) throw new CultureNotFoundException(ResourceNotFoundMessage);
                System.Xml.XmlNodeList xn = xdoc.GetElementsByTagName("territory");
                string[] countries = new string[xn.Count];
                for (int i = 0; i < xn.Count; i++)
                {
                    countries[i] = xn[i].InnerXml;
                }
                return countries;
            }
            /// <summary>
            /// Get all the countries by iso language
            /// </summary>
            /// <param name="codes">The ISO countries codes</param>
            /// <param name="ISOlanguage">the iso language</param>
            public static string[] CountriesByLanguage(string ISOlanguage, params string[] codes)
            {
                if (string.IsNullOrEmpty(ISOlanguage) || !codes.Any())
                {
                    throw new ArgumentException(ArgumentExceptionMessage);
                }
                var xdoc = GetEntryXmlDoc(GetResourceFile(ISOlanguage.ToLower() + ".xml"));
                if (xdoc == null) throw new CultureNotFoundException(ResourceNotFoundMessage);
                List<string> countries = new List<string>();
                for (int i = 0; i < codes.Length; i++)
                {
                    var country = xdoc?.SelectSingleNode("//territory[@type='" + codes[i].ToUpper() + "']")?.InnerText;
                    if (!string.IsNullOrEmpty(country))
                    {
                        countries.Add(country);
                    }
                }
                return countries.ToArray();
            }

            static byte[] GetResourceFile(string filename)
            {
                using (Stream stream = typeof(CLRD).Assembly.
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
}
