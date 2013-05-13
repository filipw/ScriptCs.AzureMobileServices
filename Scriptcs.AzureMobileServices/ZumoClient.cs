using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Scriptcs.AzureMobileServices
{
    public class ZumoClient<T>
    {
        private readonly string _url;
        private readonly HttpClient _client;

        internal ZumoClient(string accountName)
            : this(accountName, null, null)
        {            
        }

        internal ZumoClient(string accountName, string key)
            : this(accountName, key, null)
        {
        }

        internal ZumoClient(string accountName, string key, string tableName)
        {
            if (string.IsNullOrWhiteSpace(tableName))
                _url = string.Format("https://{0}.azure-mobile.net/tables/{1}/",accountName, typeof(T).Name.ToLower());
            else
                _url = string.Format("https://{0}.azure-mobile.net/tables/{1}/", accountName, tableName);

            _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            if (!string.IsNullOrWhiteSpace(key))
            {
                _client.DefaultRequestHeaders.Add("X-ZUMO-APPLICATION", key);
            }
        }

        public IEnumerable<T> Get()
        {
            var data = _client.GetStringAsync(_url).Result;
            var item = JsonConvert.DeserializeObject<IEnumerable<T>>(data);
            return item;
        }

        public T Get(int id)
        {
            var data = _client.GetStringAsync(_url + id).Result;
            var item = JsonConvert.DeserializeObject<T>(data);
            return item;
        }

        public T Add(T item)
        {
            var obj = JsonConvert.SerializeObject(item, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            var request = new HttpRequestMessage(HttpMethod.Post, _url);
            request.Content = new StringContent(obj, Encoding.UTF8, "application/json");

            var data = _client.SendAsync(request).Result;

            if (!data.IsSuccessStatusCode)
                throw new HttpRequestException(data.StatusCode.ToString());

            return data.Content.ReadAsAsync<T>().Result;
        }

        public T Update(int id, T item)
        {
            var request = new HttpRequestMessage(new HttpMethod("PATCH"), _url + id);
            var obj = JsonConvert.SerializeObject(item, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            request.Content = new StringContent(obj, Encoding.UTF8, "application/json");

            var data = _client.SendAsync(request).Result;

            if (!data.IsSuccessStatusCode)
                throw new HttpRequestException(data.StatusCode.ToString());

            return data.Content.ReadAsAsync<T>().Result;
        }

        public void Remove(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Delete, _url + id);
            var data = _client.SendAsync(request).Result;

            if (!data.IsSuccessStatusCode)
                throw new HttpRequestException(data.StatusCode.ToString());
        }
    }
}
