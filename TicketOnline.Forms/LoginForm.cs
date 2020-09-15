using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TicketOnline.Forms
{
    public class LoginForm
    {
        public string Email { get; set; }
        public string Passwd { get; set; }
    }
}
