using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Global
{
    public class GetPaymentMethod
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVVnumber { get; set; }
        public string User { get; set; }
        public string Email { get; set; }
    }
}
