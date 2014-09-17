using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace UnotellyIpUpdater
{
    public class IpAddressFetcher
    {
        public async Task<IpAddressInfo> GetCurrentIpAddressDetails()
        {
            var client = new HttpClient();
            var result = await client.GetAsync("http://ipinfo.io/json/");
            var value = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<IpAddressInfo>(value);
        }
    }
}