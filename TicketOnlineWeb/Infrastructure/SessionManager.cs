using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOnline.Models.Data;
using TicketOnline.Models.Data.Mappers;
using G = TicketOnline.Models.Global;

namespace TicketOnlineWeb.Infrastructure
{
    public class SessionManager : ISessionManager
    {
        private ISession Session;

        public SessionManager(IHttpContextAccessor httpContextAccessor)
        {
            Session = httpContextAccessor.HttpContext.Session;
        }

        public User User
        {
            get
            {
                //return (Session.Keys.Contains(nameof(User))) ? JsonConvert.DeserializeObject<User>(Session.GetString(nameof(User))) : null;
                string json = Session.GetString(nameof(User));
                return (json is null) ? null : JsonConvert.DeserializeObject<G.User>(json).ToClient();
            }

            set
            {
                Session.SetString(nameof(User), JsonConvert.SerializeObject(value));
            }
        }

        private int _eventId;

        public int EventId
        {
            get { return _eventId; }
            set { _eventId = value; }
        }

        private int _commentId;

        public int CommentId
        {
            get { return _commentId; }
            set { _commentId = value; }
        }


        public void Abandon()
        {
            Session.Clear();
        }
    }
}
