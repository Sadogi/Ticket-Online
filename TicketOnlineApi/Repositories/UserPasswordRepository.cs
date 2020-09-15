using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketOnline.Interfaces;
using TicketOnline.Models.Global;

namespace TicketOnlineApi.Repositories
{
    public class UserPasswordRepository : IUpdatePassworddRepository<Password>
    {
        private SqlConnection _connection;

        public UserPasswordRepository()
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
        }

        public bool UpdatePasswd(int id, Password entity)
        {
            using (_connection)
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_UpdatePasswd";

                cmd.Parameters.AddWithValue("@NewPasswd", entity.NewPasswd);
                cmd.Parameters.AddWithValue("@Passwd", entity.Passwd);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() == 1;
            }
        }
    }
}
