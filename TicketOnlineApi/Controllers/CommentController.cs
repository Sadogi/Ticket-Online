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
    public class CommentController : ControllerBase
    {
        private IBookingCommentRepository<Comment, GetComment> _commentRepository;

        public CommentController()
        {
            _commentRepository = new CommentRepository();
        }

        // GET: api/Comment/5
        [HttpGet("event/{eventId}", Name = "GetAllEventComment")]
        public IEnumerable<GetComment> GetAllEventComment(int eventId)
        {
            return _commentRepository.GetAll(eventId);
        }

        // GET: api/Comment/5
        [HttpGet("user/{userId}", Name = "GetAllUserComment")]
        public IEnumerable<GetComment> GetAllUserComment(int userId)
        {
            return _commentRepository.GetAllByUser(userId);
        }

        // GET: api/Comment/5
        [HttpGet("{id}", Name = "GetComment")]
        public GetComment Get(int id)
        {
            return _commentRepository.Get(id);
        }

        // POST: api/Comment
        [HttpPost]
        public IActionResult Post([FromBody] Comment comment)
        {
            _commentRepository.Create(comment);
            return Created("api/Comment", comment);
        }

        // PUT: api/Comment/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Comment comment)
        {
            if (_commentRepository.Update(id, comment))
                return Ok();
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if (_commentRepository.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}