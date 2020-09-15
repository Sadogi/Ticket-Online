using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketOnlineApi.Models
{
    public class CreateEvent
    {
        [Required]
        [MaxLength(75)]
        public string Name { get; set; }
        [Required]
        [MaxLength(75)]
        public string Type { get; set; }
        [Required]
        [MaxLength(75)]
        public string Organizer { get; set; }        
        public DateTime? Date { get; set; }
        public string Location { get; set; }
        public int? Tickets { get; set; }
        public double? Price { get; set; }
        public string Description { get; set; }
    }
}
