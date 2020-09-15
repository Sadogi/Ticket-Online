//using Newtonsoft.Json;
//using System;
//using System.Collections.Generic;
//using System.ComponentModel;
//using System.Net.Http;
//using System.Net.Http.Headers;
//using System.Security.Authentication;
//using System.Text;
//using TicketOnline.Interfaces;

//namespace TicketOnline.Models.Repositories
//{
//    public class ApiRequester 
//    {
//        private readonly HttpClient _httpClient;
//        public ApiRequester(string url)
//        {
//            var handler = new HttpClientHandler
//            {
//                SslProtocols = SslProtocols.Default
//            };

//            handler.ServerCertificateCustomValidationCallback = (request, cert, chain, errors) =>
//            {
//                return true;
//            };

//            _httpClient = new HttpClient(handler);
//            _httpClient.BaseAddress = new Uri(url);
//            _httpClient.DefaultRequestHeaders.Accept.Clear();
//            _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//        }

//        #region Requester Steve adapté
//        //public IEnumerable<TResult> GetAll<TResult>(string url)
//        //{
//        //    using (HttpResponseMessage message = _httpClient.GetAsync(url).Result)
//        //    {
//        //        message.EnsureSuccessStatusCode();
//        //        string json = message.Content.ReadAsStringAsync().Result;
//        //        return JsonConvert.DeserializeObject<TResult[]>(json);
//        //    }
//        //}

//        //public TResult Get<TResult>(string url)
//        //{
//        //    using (HttpResponseMessage message = _httpClient.GetAsync(url).Result)
//        //    {
//        //        message.EnsureSuccessStatusCode();
//        //        string json = message.Content.ReadAsStringAsync().Result;
//        //        return JsonConvert.DeserializeObject<TResult>(json);
//        //    }
//        //}

//        //public TBody Create<TBody>(TBody body, string url)
//        //{
//        //    string jsonBody = JsonConvert.SerializeObject(body);
//        //    HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
//        //    HttpResponseMessage response = _httpClient.PostAsync(url, content).Result;
//        //    response.EnsureSuccessStatusCode();

//        //    return JsonConvert.DeserializeObject<TBody>(jsonBody);

//        //}

//        //public bool Update<TBody>(TBody body, string url)
//        //{
//        //    string jsonBody = JsonConvert.SerializeObject(body);
//        //    HttpContent content = new StringContent(jsonBody, Encoding.UTF8, "application/json");
//        //    HttpResponseMessage response = _httpClient.PutAsync(url, content).Result;

//        //    return response.IsSuccessStatusCode;
//        //}

//        //public bool Delete(string url)
//        //{
//        //    HttpResponseMessage response = _httpClient.DeleteAsync(url).Result;

//        //    return response.IsSuccessStatusCode;
//        //}
//        #endregion


//        public IEnumerable<TResult> GetAll<TResult>(string url)
//        {
//            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(url).Result;
//            httpResponseMessage.EnsureSuccessStatusCode();
//            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

//            return JsonConvert.DeserializeObject<TResult[]>(json);
//        }

//        public TResult Get<TResult>(string url)
//        {
//            HttpResponseMessage httpResponseMessage = _httpClient.GetAsync(url).Result;
//            httpResponseMessage.EnsureSuccessStatusCode();
//            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

//            return JsonConvert.DeserializeObject<TResult>(json);
//        }

//        public TEntity Create<TEntity>(TEntity entity, string url)
//        {
//            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
//            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

//            HttpResponseMessage httpResponseMessage = _httpClient.PostAsync(url, content).Result;
//            httpResponseMessage.EnsureSuccessStatusCode();
//            string json = httpResponseMessage.Content.ReadAsStringAsync().Result;

//            return JsonConvert.DeserializeObject<TEntity>(json);
//        }

//        public bool Update<TEntity>(TEntity entity, string url)
//        {
//            HttpContent content = new StringContent(JsonConvert.SerializeObject(entity));
//            content.Headers.ContentType = new MediaTypeWithQualityHeaderValue("application/json");

//            HttpResponseMessage httpResponseMessage = _httpClient.PutAsync(url, content).Result;
//            return httpResponseMessage.IsSuccessStatusCode;
//        }

//        public bool Delete(string url)
//        {
//            HttpResponseMessage httpResponseMessage = _httpClient.DeleteAsync(url).Result;
//            return httpResponseMessage.IsSuccessStatusCode;
//        }
//    }
//}
