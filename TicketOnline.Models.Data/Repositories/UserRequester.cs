using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data.Mappers;
using G = TicketOnline.Models.Global;

namespace TicketOnline.Models.Data.Repositories
{
    public class UserRequester : IUserEventRepository<User>
    {
        private readonly HttpClient _httpClient;

        public UserRequester(string url)
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

        public IEnumerable<User> GetAll()
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("user").Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.User[]>(json).Select(e => e.ToClient());
        }

        public User Get(int id)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("user/" + id).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.User>(json)?.ToClient();
        }

        public User Create(User entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity.ToGlobal()));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("user", content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();

            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<G.User>(json).ToClient();
        }

        public bool Update(int id, User entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity.ToGlobal()));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PutAsync("user/" + id, content).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public bool Delete(int id)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.DeleteAsync("user/" + id).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}
