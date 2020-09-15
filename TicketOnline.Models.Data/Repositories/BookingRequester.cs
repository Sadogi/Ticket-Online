using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using TicketOnline.Interfaces;
using TicketOnline.Models.Global;

namespace TicketOnline.Models.Data.Repositories
{
    public class BookingRequester : IBookingCommentRepository<Booking, GetBooking>
    {
        private readonly HttpClient _httpClient;

        public BookingRequester(string url)
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

        public IEnumerable<GetBooking> GetAll(int eventId)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionTool.user.token);
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("booking/event/" + eventId).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<GetBooking[]>(json);
        }

        public IEnumerable<GetBooking> GetAllByUser(int userId)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("booking/user/" + userId).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<GetBooking[]>(json);
        }

        public GetBooking Get(int id)
        {
            //_httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", SessionTool.user.token);
            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync("booking/" + id).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<GetBooking>(json);
        }

        public Booking Create(Booking entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync("booking", content).Result;
            httpResponseMessage.EnsureSuccessStatusCode();
            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

            return JsonConvert.DeserializeObject<Booking>(json);
        }

        public bool Update(int id, Booking entity)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage httpResponseMessage = _httpClient.PutAsync("booking/" + id, content).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }

        public bool Delete(int id)
        {
            HttpResponseMessage httpResponseMessage = _httpClient.DeleteAsync("booking/" + id).Result;
            return httpResponseMessage.IsSuccessStatusCode;
        }
    }
}
