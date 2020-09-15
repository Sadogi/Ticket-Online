using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Interfaces
{
    public interface IAuthenticateRepository<TRegisterForm, TLoginForm, TResult> : IAuthRepository<TRegisterForm, TLoginForm, TResult>
    {
        TResult Authenticate(TResult user);
    }
}
