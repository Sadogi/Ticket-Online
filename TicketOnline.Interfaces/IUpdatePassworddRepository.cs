using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnline.Interfaces
{
    public interface IUpdatePassworddRepository<TEntity>
    {
        bool UpdatePasswd(int id, TEntity entity);
    }
}
