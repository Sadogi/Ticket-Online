using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Global
{
    public class PaymentMethod
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CardHolder { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpirationDate { get; set; }
        public int CVVnumber { get; set; }
    }
}
