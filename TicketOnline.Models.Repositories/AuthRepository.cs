//using System;
//using System.Collections.Generic;
//using System.Text;
//using TicketOnline.Forms;
//using TicketOnline.Models.Global;
//using System.Net.Http;
//using System.Security.Authentication;
//using System.Net.Http.Headers;
//using Newtonsoft.Json;
//using System.Net;
//using TicketOnline.Interfaces;
//using System.Threading.Tasks;

//namespace TicketOnline.Models.Repositories
//{
//    public class AuthRepository : IAuthRepository<RegisterForm, LoginForm, User>
//    {
//        private readonly HttpClient _httpClient;

//        public AuthRepository(Uri baseAddress)
//        {
//            var handler = new HttpClientHandler
//            {
//                SslProtocols = SslProtocols.Default
//            };

//            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => true;

//            _httpClient = new HttpClient(handler);
//            _httpClient.BaseAddress = baseAddress;
//            _httpClient.DefaultRequestHeaders.Accept.Clear();
//            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        }

//        public User Login(LoginForm loginForm)
//        {
//            HttpContent content = new StringContent(JsonConvert.SerializeObject(loginForm));
//            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

//            HttpResponseMessage responseMessage = _httpClient.PostAsync("auth/login", content).Result;
//            responseMessage.EnsureSuccessStatusCode();

//            if (responseMessage.StatusCode == HttpStatusCode.NoContent)
//                return null;

//            string json = responseMessage.Content.ReadAsStringAsync().Result;
//            return JsonConvert.DeserializeObject<User>(json);
//        }

//        public void Register(RegisterForm registerForm)
//        {
//            HttpContent content = new StringContent(JsonConvert.SerializeObject(registerForm));
//            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

//            HttpResponseMessage responseMessage = _httpClient.PostAsync("auth/register", content).Result;
//            responseMessage.EnsureSuccessStatusCode();
//        }

//        public User Authenticate(User user)
//        {
//            throw new NotImplementedException();
//        }
//    }
//}
