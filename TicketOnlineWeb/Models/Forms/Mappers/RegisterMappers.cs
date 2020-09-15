using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using F = TicketOnline.Forms;

namespace TicketOnlineWeb.Models.Forms.Mappers
{
    public static class RegisterMappers
    {
        public static RegisterForm ToWeb(this F.RegisterForm registerForm)
        {
            return new RegisterForm
            {
                ScreenName = registerForm.ScreenName,
                Email = registerForm.Email,
                Passwd = registerForm.Passwd
            };
        }
    }
}
