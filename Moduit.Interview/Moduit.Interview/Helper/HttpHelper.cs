using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Moduit.Interview.Helper
{
    public class HttpHelper
    {
        public static async Task<T> Get<T>(string apiBasicUri, string url)
        {
            using var client = new HttpClient();
            client.BaseAddress = new Uri(apiBasicUri);
            var result = await client.GetAsync(url);
            result.EnsureSuccessStatusCode();
            string resultContentString = await result.Content.ReadAsStringAsync();
            var settings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore
            };
            T resultContent = JsonConvert.DeserializeObject<T>(resultContentString, settings);
            return resultContent;
        }
    }
}
