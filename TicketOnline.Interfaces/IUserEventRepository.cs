using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Interfaces
{
    public interface IUserEventRepository<TEntity>
    {        
        IEnumerable<TEntity> GetAll();
        TEntity Get(int id);
        TEntity Create(TEntity entity);
        bool Update(int id, TEntity entity);
        bool Delete(int id);
    }
}
