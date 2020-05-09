using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;

namespace CLRDGAddress
{
    public class CLRD
    {
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
                var locale = new System.Globalization.CultureInfo(culture).TwoLetterISOLanguageName.ToLower();
                var xdoc = GetEntryXmlDoc(GetResourceFile(locale + ".xml"));
                var xn = xdoc.SelectSingleNode($"//territory[@type={code}]");
                return xn.InnerText;
            }
            /// <summary>
            /// Country by Language ISO code
            /// </summary>
            /// <param name="code">Country code</param>
            /// <param name="ISOlanguage">ISO language</param>
            /// <returns></returns>
            public static string CountryByLanguage(string code, string ISOlanguage)
            {
                var xdoc = GetEntryXmlDoc(GetResourceFile(ISOlanguage.ToLower() + ".xml"));
                var xn = xdoc.SelectSingleNode("//territory[@type='" + code + "']");
                return xn.InnerText;
            }
            /// <summary>
            /// Get all the countries by language
            /// </summary>
            /// <param name="ISOlanguage">language iso code</param>
            /// <returns></returns>
            public static string[] CountriesByLanguage(string ISOlanguage)
            {
                var xdoc = GetEntryXmlDoc(GetResourceFile(ISOlanguage.ToLower() + ".xml"));
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
                string[] countries = new string[codes.Length];
                var xdoc = GetEntryXmlDoc(GetResourceFile(ISOlanguage.ToLower() + ".xml"));
                for (int i = 0; i < codes.Length; i++)
                {
                    countries[i] = xdoc.SelectSingleNode("//territory[@type='" + codes[i] + "']").InnerText;
                }

                return countries;
            }

            static byte[] GetResourceFile(string filename)
            {
                using (Stream stream = typeof(CLRDGAddress.CLRD).Assembly.
                           GetManifestResourceStream("CLRDGAddress.CLRD." + filename))
                {
                    if (stream == null) return null;
                    byte[] buffer = new byte[stream.Length];
                    stream.Read(buffer, 0, buffer.Length);
                    return buffer;
                }
            }

            static XmlDocument GetEntryXmlDoc(byte[] Bytes)
            {
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
