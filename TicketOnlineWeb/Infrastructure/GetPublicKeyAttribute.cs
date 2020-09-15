using CryptoTools;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using TicketOnline.Models.Data.Repositories;
using TicketOnline.Models.Global;

namespace TicketOnlineWeb.Infrastructure
{
    public class GetPublicKeyAttribute : ActionFilterAttribute
    {

        //public override void OnActionExecuting(ActionExecutingContext context)
        //{
        //    ICryptoRSA encryption = (ICryptoRSA)context.HttpContext.RequestServices.GetService(typeof(ICryptoRSA));

        //    using (HttpClient httpClient = new HttpClient())
        //    {
        //        httpClient.BaseAddress = new Uri("http://localhost:51049/api/");
        //        httpClient.DefaultRequestHeaders.Accept.Clear();
        //        httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage httpResponseMessage = httpClient.GetAsync("security").Result;
        //        httpResponseMessage.EnsureSuccessStatusCode();

        //        string json = httpResponseMessage.Content.ReadAsStringAsync().Result;
        //        PublicKeyInfo publicKeyInfo = JsonConvert.DeserializeObject<PublicKeyInfo>(json);

        //        encryption.ImportBinaryKeys(Convert.FromBase64String(publicKeyInfo.PublicKey));
        //    }

        //}

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            SecurityRequester securityRepository = (SecurityRequester)context.HttpContext.RequestServices.GetService(typeof(SecurityRequester));
            ICryptoRSA cryptoRSA = (ICryptoRSA)context.HttpContext.RequestServices.GetService(typeof(ICryptoRSA));
            PublicKeyInfo publicKeyInfo = securityRepository.Get();
            cryptoRSA.ImportBinaryKeys(Convert.FromBase64String(publicKeyInfo.PublicKey));
        }
    }
}
