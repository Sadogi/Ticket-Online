using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;

namespace TicketOnline.Models.Data.Repositories
{
    public class ApiRequester
    {
        private readonly HttpClient _httpClient;

        public ApiRequester(string url)
        {
            var handler = new HttpClientHandler
            {
                SslProtocols = SslProtocols.Default
            };

            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
            {
                return true;
            };

            _httpClient = new HttpClient(handler);
            _httpClient.BaseAddress = new Uri(url);
            _httpClient.DefaultRequestHeaders.Accept.Clear();
            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
           
        }

        public IEnumerable<TEntity> GetAll<TEntity>(string url)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(url).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<TEntity[]>(json);
        }

        public TEntity Get<TEntity>(string url)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionTool.user.token);
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(url).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<TEntity>(json);
        }

        public TEntity Create<TEntity>(TEntity entity, string url)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync(url, content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<TEntity>(json);
        }

        public bool Update<TEntity>(TEntity entity, string url)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PutAsync(url, content).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public bool Delete<TEntity>(string url)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.DeleteAsync(url).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}
