using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketOnline.Forms
{
    public class RegisterForm
    {
        public string ScreenName { get; set; }
        public string Email { get; set; }
        public string Passwd { get; set; }
    }
}
