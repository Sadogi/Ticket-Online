using System;
using System.Collections.Generic;
using System.Text;

namespace TicketOnlineWpf.Utils
{
    public class GestionEvent<TEntity> where TEntity : class
    {
        private static GestionEvent<TEntity> _instance;

        public static GestionEvent<TEntity> Instance
        {
            get
            {
                return _instance ?? (_instance = new GestionEvent<TEntity>());
            }
        }

        private Action<TEntity> DeleteEvent;

        public void Register(Action<TEntity> t)
        {
            DeleteEvent += t;
        }

        public void Send(TEntity _entity)
        {
            DeleteEvent?.Invoke(_entity);
        }
    }
}
