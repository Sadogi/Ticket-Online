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
    public class EventRequester : IUserEventRepository<Event>
    {
        private readonly HttpClient _httpClient;

        public EventRequester(string url)
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

        public IEnumerable<Event> GetAll()
        {            
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("event").Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.Event[]>(json).Select(e => e.ToClient());
        }

        public Event Get(int id)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("event/" + id).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.Event>(json)?.ToClient();
        }

        public Event Create(Event entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity.ToGlobal()));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("event", content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<G.Event>(json).ToClient();
        }

        public bool Update(int id, Event entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity.ToGlobal()));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PutAsync("event/" + id, content).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public bool Delete(int id)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.DeleteAsync("event/" + id).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}
