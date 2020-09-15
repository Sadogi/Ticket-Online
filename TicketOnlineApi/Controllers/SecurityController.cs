using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CryptoTools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketOnline.Models.Global;

namespace TicketOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        private readonly ICryptoRSA _cryptoService;

        public SecurityController(ICryptoRSA cryptoService)
        {
            _cryptoService = cryptoService;
        }

        [HttpGet]
        public PublicKeyInfo Get()
        {
            PublicKeyInfo publicKeyInfo = new PublicKeyInfo() { PublicKey = Convert.ToBase64String(_cryptoService.BinaryPublicKey) };
            return publicKeyInfo;
        }
    }
}