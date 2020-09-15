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
    public class UserRepository : IUserEventRepository<User>
    {
        private SqlConnection _connection;

        public UserRepository()
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
        }

        public User Create(User entity)
        {
            using (_connection)
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_CreateUser";

                cmd.Parameters.AddWithValue("@LastName", entity.LastName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FirstName", entity.FirstName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ScreenName", entity.ScreenName);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                cmd.Parameters.AddWithValue("@Passwd", entity.Passwd);
                cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);

                cmd.ExecuteNonQuery();

                return entity;
            }
        }

        public IEnumerable<User> GetAll()
        {
            List<User> EventList = new List<User>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllUser";

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    EventList.Add(new User
                    {
                        Id = (int)dr["Id"],
                        LastName = (dr["LastName"] is DBNull) ? null : (string)dr["LastName"],
                        FirstName = (dr["FirstName"] is DBNull) ? null : (string)dr["FirstName"],
                        ScreenName = (string)dr["ScreenName"],
                        Email = (string)dr["Email"],
                        Address = (dr["Address"] is DBNull) ? null : (string)dr["Address"],
                        IsActive = (bool)dr["IsActive"],
                        IsAdmin = (bool)dr["IsAdmin"],
                    });
                }
            }
            return EventList;
        }

        public User Get(int id)
        {
            User user = new User();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetUser";

            cmd.Parameters.AddWithValue("@Id", id);
            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    user.Id = (int)dr["Id"];
                    user.LastName = (dr["LastName"] is DBNull) ? null : (string)dr["LastName"];
                    user.FirstName = (dr["FirstName"] is DBNull) ? null : (string)dr["FirstName"];
                    user.ScreenName = (string)dr["ScreenName"];
                    user.Email = (string)dr["Email"];
                    user.Address = (dr["Address"] is DBNull) ? null : (string)dr["Address"];
                    user.IsActive = (bool)dr["IsActive"];
                    user.IsAdmin = (bool)dr["IsAdmin"];
                }
            }
            return user;
        }

        public bool Update(int id, User entity)
        {
            using (_connection)
            {
                SqlCommand cmd = _connection.CreateCommand();
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "SP_UpdateUser";

                cmd.Parameters.AddWithValue("@LastName", entity.LastName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@FirstName", entity.FirstName ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@ScreenName", entity.ScreenName);
                cmd.Parameters.AddWithValue("@Email", entity.Email);
                //cmd.Parameters.AddWithValue("@Passwd", entity.Passwd);
                cmd.Parameters.AddWithValue("@Address", entity.Address ?? (object)DBNull.Value);
                cmd.Parameters.AddWithValue("@IsActive", entity.IsActive);
                cmd.Parameters.AddWithValue("@IsAdmin", entity.IsAdmin);
                cmd.Parameters.AddWithValue("@Id", id);

                return cmd.ExecuteNonQuery() == 1;
            }
        }

        public bool Delete(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_DeleteUser";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}