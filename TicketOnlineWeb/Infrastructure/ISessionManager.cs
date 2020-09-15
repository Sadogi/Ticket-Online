using TicketOnline.Models.Data;

namespace TicketOnlineWeb.Infrastructure
{
    public interface ISessionManager
    {
        User User { get; set; }
        int EventId { get; set; }
        void Abandon();
    }
}