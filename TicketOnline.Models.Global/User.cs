using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Models.Global
{
    public class User
    {
        public int Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string ScreenName { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public bool IsAdmin { get; set; }

        public string Token { get; set; }
    }
}
