using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Interfaces
{
    public interface IBookingCommentRepository<TEntity, TGet>
    {
        IEnumerable<TGet> GetAllByUser(int userId);
        IEnumerable<TGet> GetAll(int fkId);
        TGet Get(int id);
        TEntity Create(TEntity entity);
        bool Update(int id, TEntity entity);
        bool Delete(int id);
    }
}
