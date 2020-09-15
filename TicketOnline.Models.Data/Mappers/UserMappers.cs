using System;
using System.Collections.Generic;
using System.Text;
using G = TicketOnline.Models.Global;

namespace TicketOnline.Models.Data.Mappers
{
    public static class UserMappers
    {
        public static G.User ToGlobal(this User u)
        {
            return new G.User
            {
                Id = u.Id,
                LastName = u.LastName,
                FirstName = u.FirstName,
                ScreenName = u.ScreenName,
                Email = u.Email,
                Passwd = u.Passwd,
                Address = u.Address,
                IsActive = u.IsActive,
                IsAdmin = u.IsAdmin
            };
        }

        public static User ToClient(this G.User u)
        {
            return new User(u.Id, u.LastName, u.FirstName, u.ScreenName, u.Email, u.Passwd, u.Address, u.IsActive, u.IsAdmin, u.Token);
        }
    }
}
