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
    public class BookingController : ControllerBase
    {
        private IBookingCommentRepository<Booking, GetBooking> _bookingRepository;

        public BookingController()
        {
            _bookingRepository = new BookingRepository();
        }

        // GET: api/Booking/5
        [HttpGet("event/{eventId}", Name = "GetAllEventBooking")]
        public IEnumerable<GetBooking> GetAllEventBooking(int eventId)
        {
            return _bookingRepository.GetAll(eventId);
        }

        // GET: api/Booking/5
        [HttpGet("user/{userId}", Name = "GetAllUserBooking")]
        public IEnumerable<GetBooking> GetAllUserBooking(int userId)
        {
            return _bookingRepository.GetAllByUser(userId);
        }

        // GET: api/Booking/5
        [HttpGet("{id}", Name = "GetBooking")]
        public GetBooking Get(int id)
        {
            return _bookingRepository.Get(id);
        }

        // POST: api/Booking
        [HttpPost]
        public IActionResult Post([FromBody] Booking booking)
        {
            _bookingRepository.Create(booking);
            return Created("api/Booking", booking);
        }

        // PUT: api/Booking/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Booking booking)
        {
            if (_bookingRepository.Update(id, booking))
                return Ok();
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_bookingRepository.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
