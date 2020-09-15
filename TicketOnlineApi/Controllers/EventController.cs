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
using TicketOnlineApi.Models;
using TicketOnlineApi.Models.Mappers;
using TicketOnlineApi.Repositories;

namespace TicketOnlineApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private IUserEventRepository<Event> _eventRepository;

        public EventController()
        {
            _eventRepository = new EventRepository();
        }

        // GET: api/Event
        [HttpGet]
        public IEnumerable<Event> GetAll()
        {
            return _eventRepository.GetAll();
        }

        // GET: api/Event/5
        [HttpGet("{id}", Name = "GetEvent")]
        public Event Get(int id)
        {
            return _eventRepository.Get(id);
        }

        // POST: api/Event
        [HttpPost]
        public IActionResult Post([FromBody] Event e)
        {
            _eventRepository.Create(e);
            return Created("api/Event", e);
        }

        // PUT: api/Event/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Event e)
        {
            if(_eventRepository.Update(id, e))
                return Ok();
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_eventRepository.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
