using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketOnline.Models.Global;

namespace TicketOnlineApi.Models.Mappers
{
    public static class CreateEventMappers
    {
        public static CreateEvent ToApi(this Event e)
        {
            return new CreateEvent
            {
                Name = e.Name,
                Type = e.Type,
                Organizer = e.Organizer,                
                Date = e.Date,
                Location = e.Location,
                Tickets = e.Tickets,
                Price = e.Price,
                Description = e.Description
            };
        }

        public static Event ToDb(this CreateEvent e)
        {
            return new Event
            {
                Name = e.Name,
                Type = e.Type,
                Organizer = e.Organizer,                
                Date = e.Date,
                Location = e.Location,
                Tickets = e.Tickets,
                Price = e.Price,
                Description = e.Description
            };
        }
    }
}
