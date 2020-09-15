using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using TicketOnline.Models.Global;

namespace TicketOnlineWeb.Models.Event
{
    public class EventWithComment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Organizer { get; set; }
        public DateTime? Date { get; set; }
        public string Location { get; set; }
        public int? Tickets { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }

        public IEnumerable<GetComment> CommentList { get; set; }
    }
}
