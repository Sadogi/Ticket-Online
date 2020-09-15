using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using CryptoTools;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketOnline.Interfaces;
using TicketOnline.Models.Global;
using TicketOnlineApi.Repositories;

namespace TicketOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserEventRepository<User> _userRepository;
        private IUpdatePassworddRepository<Password> _passwordRepository;
        private readonly ICryptoRSA _cryptoService;

        public UserController(ICryptoRSA cryptoService)
        {
            _cryptoService = cryptoService;
            _userRepository = new UserRepository();
            _passwordRepository = new UserPasswordRepository();
        }
        // GET: api/User
        [HttpGet]
        public IEnumerable<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        // GET: api/User/5
        [HttpGet("{id}", Name = "GetUser")]
        public User Get(int id)
        {
            return _userRepository.Get(id);
        }

        // POST: api/User
        [HttpPost]
        public IActionResult Post([FromBody] User user)
        {
            _userRepository.Create(user);
            return Created("api/User", user);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] User user)
        {
            if(_userRepository.Update(id, user))
                return Ok();
            else
                return NotFound();
        }

        // PUT: api/User/5
        [HttpPut("pwd/{id}")]
        public IActionResult PutPassword(int id, [FromBody] Password pwd)
        {
            pwd.Passwd = _cryptoService.Decrypter(Convert.FromBase64String(pwd.Passwd));
            pwd.NewPasswd = _cryptoService.Decrypter(Convert.FromBase64String(pwd.NewPasswd));

            if (_passwordRepository.UpdatePasswd(id, pwd))
                return Ok();
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_userRepository.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
