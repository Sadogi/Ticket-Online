using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Authentication;
using System.Text;
using TicketOnline.Forms;
using TicketOnline.Interfaces;
using TicketOnline.Models.Data.Mappers;
using TicketOnline.Models.Global;
using G = TicketOnline.Models.Global;

namespace TicketOnline.Models.Data.Repositories
{
    public class AuthRequester : IAuthRepository<RegisterForm, LoginForm, User>
    {
        private readonly HttpClient _httpClient;

        public AuthRequester(string url)
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



        public User Login(LoginForm loginForm)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(loginForm));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage responseMessage = _httpClient.PostAsync("auth/login", content).Result;
            ////***if av responseMessage.EnsureSuccessStatusCode(); si BadRequest/NotFound côté api sans Try/Catch côté Web car exception non gérée
            //if (responseMessage.StatusCode == HttpStatusCode.BadRequest)
            //    return null;

            responseMessage.EnsureSuccessStatusCode();

            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
                return null;

            string json = responseMessage.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<G.User>(json).ToClient();
        }

        public void Register(RegisterForm registerForm)
        {
            HttpContent content = new StringContent(JsonConvert.SerializeObject(registerForm));
            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

            HttpResponseMessage responseMessage = _httpClient.PostAsync("auth/register", content).Result;
            responseMessage.EnsureSuccessStatusCode();
        }
    }
}