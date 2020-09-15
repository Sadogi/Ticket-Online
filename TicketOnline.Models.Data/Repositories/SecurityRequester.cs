using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using TicketOnline.Models.Global;

namespace TicketOnline.Models.Data.Repositories
{
    public class SecurityRequester
    {
        private readonly HttpClient _httpClient;

        public SecurityRequester(string url)
        {
            HttpClientHandler handler = new HttpClientHandler()
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

        //public PublicKeyInfo Get()
        //{
        //    Task<HttpResponseMessage> httpResponseMessageTask = _httpClient.GetAsync("security");
        //    httpResponseMessageTask.Wait();
        //    HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;
        //    httpResponseMessage.EnsureSuccessStatusCode();

        //    return JsonConvert.DeserializeObject<PublicKeyInfo>(httpResponseMessage.Content.ReadAsStringAsync().Result);
        //}

        public PublicKeyInfo Get()
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("security").Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<PublicKeyInfo>(json);
        }
    }
}
