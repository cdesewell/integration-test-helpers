using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using IntegrationTestHelpers.Converters;
using Newtonsoft.Json;

namespace IntegrationTestHelpers.Clients
{
    public class HttpApiClient
    {
        public static async Task<(T Body, HttpResponseMessage response)> Get<T>(HttpClient client, Uri endpoint, string authToken)
        {
            client.SetBearerToken(authToken);

            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var response = await client.SendAsync(request);

            var body = default(T);

            if (!response.IsSuccessStatusCode) return (body, response);

            var content = await response.Content.ReadAsStringAsync();

            body = JsonConvert.DeserializeObject<T>(content);

            return (body, response);
        }

        public static async Task<(Dictionary<string, object> Body, HttpResponseMessage Response)> Get(HttpClient client, Uri endpoint, string authToken)
        {
            client.SetBearerToken(authToken);

            var request = new HttpRequestMessage(HttpMethod.Get, endpoint);

            var response = await client.SendAsync(request);

            var body = default(Dictionary<string, object>);

            if (!response.IsSuccessStatusCode) return (body, response);

            var content = await response.Content.ReadAsStringAsync();

            body = JsonConvert.DeserializeObject<Dictionary<string, object>>(content, new JsonDictionaryConverter());

            return (body, response);
        }
    }
}
