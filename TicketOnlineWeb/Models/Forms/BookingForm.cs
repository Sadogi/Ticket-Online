using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TicketOnlineWeb.Models.Forms
{
    public class BookingForm
    {
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime PurchaseDate { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Minimum 1 ticket")]
        public int TicketsPurchased { get; set; }
        public double TicketsPrice { get; set; }
        public double Amount { get; set; }
    }
}
