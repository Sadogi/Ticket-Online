using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TicketOnline.Models.Global;

namespace TicketOnlineWeb.Models.Forms
{
    public class BookingPayment
    {
        public int UserId { get; set; }
        public int EventId { get; set; }        
        public DateTime PurchaseDate { get; set; }        
        public int TicketsPurchased { get; set; }
        public double TicketsPrice { get; set; }
        public double Amount { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(30)]
        public string CardHolder { get; set; }
        [Required]
        [MinLength(16)]
        [MaxLength(19)]
        [DataType(DataType.CreditCard)]
        public string CardNumber { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime ExpirationDate { get; set; }
        [Required]
        [Range(100, 999, ErrorMessage = "3 Digits")]
        public int CVVnumber { get; set; }
    }
}
