using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Interfaces
{
    public interface IPaymentRepository<TEntity, TGet>
    {
        IEnumerable<TGet> GetAll(int fkId);
        TGet Get(int id);
        TEntity Create(TEntity entity);
        bool Update(int id, TEntity entity);
        bool Delete(int id);
    }
}
