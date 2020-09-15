using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TicketOnline.Interfaces;
using TicketOnline.Models.Global;
using TicketOnlineApi.Repositories;

namespace TicketOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private IPaymentRepository<PaymentMethod, GetPaymentMethod> _paymentMethodRepository;

        public PaymentMethodController()
        {
            _paymentMethodRepository = new PaymentMethodRepository();
        }

        // GET: api/PaymentMethod/5
        [HttpGet("all/{userId}", Name = "GetAllPaymentMethod")]
        public IEnumerable<GetPaymentMethod> GetAll(int userId)
        {
            return _paymentMethodRepository.GetAll(userId);
        }

        // GET: api/PaymentMethod/5
        [HttpGet("{id}", Name = "GetPaymentMethod")]
        public GetPaymentMethod Get(int id)
        {
            return _paymentMethodRepository.Get(id);
        }

        // POST: api/PaymentMethod
        [HttpPost]
        public IActionResult Post([FromBody] PaymentMethod paymentMethod)
        {
            _paymentMethodRepository.Create(paymentMethod);
            return Created("api/PaymentMethod", paymentMethod);
        }

        // PUT: api/PaymentMethod/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] PaymentMethod paymentMethod)
        {
            if(_paymentMethodRepository.Update(id, paymentMethod))
                return Ok();
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_paymentMethodRepository.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
