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
    public class ReplyController : ControllerBase
    {
        private IBookingCommentRepository<Reply, GetReply> _replyRepository;

        public ReplyController()
        {
            _replyRepository = new ReplyRepository();
        }

        // GET: api/Reply/5
        [HttpGet("comment/{commentId}", Name = "GetAllCommentReply")]
        public IEnumerable<GetReply> GetAllCommentReply(int commentId)
        {
            return _replyRepository.GetAll(commentId);
        }

        // GET: api/Reply/5
        [HttpGet("user/{userId}", Name = "GetAllUserReply")]
        public IEnumerable<GetReply> GetAllUserReply(int userId)
        {
            return _replyRepository.GetAllByUser(userId);
        }

        // GET: api/Reply/5
        [HttpGet("{id}", Name = "GetReply")]
        public GetReply Get(int id)
        {
            return _replyRepository.Get(id);
        }

        // POST: api/Reply
        [HttpPost]
        public IActionResult Post([FromBody] Reply reply)
        {
            _replyRepository.Create(reply);
            return Created("api/Reply", reply);
        }

        // PUT: api/Reply/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Reply reply)
        {
            if(_replyRepository.Update(id, reply))
                return Ok();
            else
                return NotFound();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            if(_replyRepository.Delete(id))
                return Ok();
            else
                return NotFound();
        }
    }
}
