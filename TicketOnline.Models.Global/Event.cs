using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Global
{
    public class Event
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
    }
}
