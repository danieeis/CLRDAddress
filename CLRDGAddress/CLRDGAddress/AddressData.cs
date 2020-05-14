using CLRDGAddress.Abstractions.GDAddress;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CLRDGAddress.Abstractions.GDAddress.JsonService;

namespace CLRDGAddress
{
    public class AddressData
    {
        private static ISerializer serializer = new JsonSerializer();
        /// <summary>
        /// Get the addresses of country
        /// </summary>
        /// <param name="code">country iso code</param>
        /// <returns></returns>
        public async static Task<Address> GetAddresses(string code)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(GDA.REST_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                Address Addresses = null;
                var path = System.IO.Path.Combine(GDA.REST_ROUTE, code.ToUpper());
                HttpResponseMessage response = await client.GetAsync(path);
                
                if (response.IsSuccessStatusCode)
                {
                    var data = await response.Content.ReadAsStringAsync();
                    Addresses = serializer.Deserialize<Address>(data);
                }
                return Addresses;
            } 
        }
        /// <summary>
        /// Get subdivisioin iso id
        /// </summary>
        /// <param name="country_code">iso code</param>
        /// <param name="sub_key">isoid code</param>
        /// <returns></returns>
        internal async static Task<string> GetSubDivisionIsoID(string country_code, string sub_key)
        {
            using (HttpClient client = new HttpClient())
            {
                SubDivision subDivision = null;
                client.BaseAddress = new Uri(GDA.REST_URL);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var path = System.IO.Path.Combine(GDA.REST_ROUTE, $"{country_code.ToUpper()}/{sub_key}");
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
}
