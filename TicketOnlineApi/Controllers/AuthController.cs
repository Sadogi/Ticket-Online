using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoTools;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TicketOnline.Forms;
using TicketOnline.Interfaces;
using TicketOnline.Models.Global;
using TicketOnlineApi.Repositories;

namespace TicketOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        IAuthenticateRepository<RegisterForm, LoginForm, User> _authRepository;
        //private IUserEventRepository<User> _userRepository;
        private readonly ILogger<SecurityController> _logger;
        private readonly ICryptoRSA _cryptoService;

        public AuthController(ILogger<SecurityController> logger, ICryptoRSA cryptoService, IAuthenticateRepository<RegisterForm, LoginForm, User> authRepository)
        {
            _authRepository = authRepository;
            _logger = logger;
            _cryptoService = cryptoService;
        }

        [HttpPost("register")]
        public IActionResult Register(RegisterForm registerForm)
        {
            if (!(registerForm is null) && ModelState.IsValid)
            {
                //_logger.LogInformation($"{registerForm.Passwd}");
                registerForm.Passwd = _cryptoService.Decrypter(Convert.FromBase64String(registerForm.Passwd));
                //_logger.LogInformation($"{registerForm.Passwd}");

                try
                {
                    _authRepository.Register(registerForm);
                    return Ok();
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }

            return (!(registerForm is null)) ? BadRequest(ModelState) : BadRequest("There is no Data !");
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody]LoginForm loginForm)
        {
            if (!(loginForm is null) && ModelState.IsValid)
            {
                loginForm.Passwd = _cryptoService.Decrypter(Convert.FromBase64String(loginForm.Passwd));

                try
                {                    
                    User user = _authRepository.Login(loginForm);
                    user = _authRepository.Authenticate(user);

                    if (user.Id == 0)
                    {
                        return NoContent();
                        //return BadRequest(new { message = "Wrong email or password" }); ////*** AuthRequester
                    }
                    else
                    {                        
                        return Ok(user);
                    }
                }
                catch (Exception ex)
                {
                    return BadRequest();
                }
            }
            return (!(loginForm is null)) ? BadRequest(ModelState) : BadRequest("There is no Data !");
        }
    }
}