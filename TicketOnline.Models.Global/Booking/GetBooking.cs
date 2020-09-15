using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Global
{
    public class GetBooking
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int EventId { get; set; }
        public DateTime PurchaseDate { get; set; }
        public int TicketsPurchased { get; set; }
        public double Amount { get; set; }
        public string Event { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
        public int CardNumber { get; set; }
    }
}
