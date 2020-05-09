using CLRDGAddress.GDAddress;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CLRDGAddress.GDAddress.JsonService;

namespace CLRDGAddress
{
    public class AddressData
    {
        private static ISerializer serializer = new JsonSerializer();
        private static HttpClient client = new HttpClient();
        /// <summary>
        /// Get the addresses of country
        /// </summary>
        /// <param name="code">country iso code</param>
        /// <returns></returns>
        public async static Task<Address> GetAddresses(string code)
        {
            Address Addresses = null;
            client.BaseAddress = new Uri(GDA.REST_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var path = System.IO.Path.Combine(GDA.REST_ROUTE, code);
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                Addresses = serializer.Deserialize<Address>(data);
            }
            return Addresses;
        }

        internal async static Task<string> GetSubDivisionIsoID(string country_code, string sub_key)
        {
            SubDivision subDivision = null;
            client.BaseAddress = new Uri(GDA.REST_URL);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));
            var path = System.IO.Path.Combine(GDA.REST_ROUTE, $"{country_code}/{sub_key}");
            HttpResponseMessage response = await client.GetAsync(path);
            if (response.IsSuccessStatusCode)
            {
                var data = await response.Content.ReadAsStringAsync();
                subDivision = serializer.Deserialize<SubDivision>(data);
            }
            return subDivision.IsoId;
        }
    }
}
