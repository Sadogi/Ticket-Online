using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F = TicketOnline.Forms;

namespace TicketOnlineWeb.Models.Forms.Mappers
{
    public static class LoginMappers
    {
        public static LoginForm ToWeb (this F.LoginForm loginForm)
        {
            return new LoginForm
            {
                Email = loginForm.Email,
                Passwd = loginForm.Passwd
            };
        }
    }
}
