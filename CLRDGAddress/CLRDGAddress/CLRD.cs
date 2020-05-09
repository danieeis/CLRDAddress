using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

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
                var strLocalXmlFile = GetResourceFile(locale + ".xml");
                var xdoc = new System.Xml.XmlDocument();
                xdoc.Load(strLocalXmlFile);
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
                var directoryCurrent = System.IO.Directory.GetCurrentDirectory();
                var strLocalXmlFile = GetResourceFile(ISOlanguage.ToLower() + ".xml");
                var xdoc = new System.Xml.XmlDocument();
                xdoc.Load(strLocalXmlFile);
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
                var strLocalXmlFile = GetResourceFile(ISOlanguage.ToLower() + ".xml");
                var xdoc = new System.Xml.XmlDocument();
                xdoc.Load(strLocalXmlFile);
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
                var strLocalXmlFile = GetResourceFile(ISOlanguage.ToLower() + ".xml");
                var xdoc = new System.Xml.XmlDocument();
                xdoc.Load(strLocalXmlFile);
                for (int i = 0; i < codes.Length; i++)
                {
                    countries[i] = xdoc.SelectSingleNode("//territory[@type='" + codes[i] + "']").InnerText;
                }

                return countries;
            }

            static string GetResourceFile(string filename)
            {
                string result = string.Empty;

                using (Stream stream = typeof(CLRDGAddress.CLRD).Assembly.
                           GetManifestResourceStream("CLRDGAddress.CLRD." + filename))
                {
                    using (StreamReader sr = new StreamReader(stream))
                    {
                        result = sr.ReadToEnd();
                    }
                }
                return result;
            }
        }
    }
}
