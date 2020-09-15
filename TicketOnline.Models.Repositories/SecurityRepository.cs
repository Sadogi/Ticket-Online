//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Security.Authentication;
//using System.Text;
//using System.Threading.Tasks;
//using TicketOnline.Models.Global;

//namespace TicketOnline.Models.Repositories
//{
//    public class SecurityRepository
//    {
//        private readonly HttpClient _httpClient;

//        public SecurityRepository(Uri baseAddress)
//        {
//            HttpClientHandler handler = new HttpClientHandler()
//            {
//                SslProtocols = SslProtocols.Default
//            };

//            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) => true;

//            _httpClient = new HttpClient(handler);
//            _httpClient.BaseAddress = baseAddress;
//            _httpClient.DefaultRequestHeaders.Accept.Clear();
//            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        }

//        public PublicKeyInfo Get()
//        {
//            Task<HttpResponseMessage> httpResponseMessageTask = _httpClient.GetAsync("security");
//            httpResponseMessageTask.Wait();
//            HttpResponseMessage httpResponseMessage = httpResponseMessageTask.Result;
//            httpResponseMessage.EnsureSuccessStatusCode();

//            return JsonConvert.DeserializeObject<PublicKeyInfo>(httpResponseMessage.Content.ReadAsStringAsync().Result);
//        }
//    }
//}
