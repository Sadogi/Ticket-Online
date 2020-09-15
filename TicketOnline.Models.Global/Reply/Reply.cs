using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Global
{
    public class Reply
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int CommentId { get; set; }
        public DateTime Date { get; set; }
        public string Content { get; set; }

    }
}
