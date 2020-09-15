using System;
using System.Collections.Generic;
using System.Text;
using G = TicketOnline.Models.Global;

namespace TicketOnline.Models.Data.Mappers
{
    public static class EventMappers
    {
        public static G.Event ToGlobal(this Event e)
        {
            return new G.Event
            {
                Id = e.Id,
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

        public static Event ToClient(this G.Event e)
        {
            return new Event(e.Id, e.Name, e.Type, e.Organizer, e.Date, e.Location, e.Tickets, e.Price, e.Description);
        }
    }
}
