using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Global
{
    public class Booking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int TicketsPurchased { get; set; }
        public double TicketsPrice { get; set; }
        public double Amount { get; set; }
    }
}
