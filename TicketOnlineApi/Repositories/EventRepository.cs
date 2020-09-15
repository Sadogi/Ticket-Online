using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using TicketOnline.Interfaces;
using TicketOnline.Models.Global;

namespace TicketOnlineApi.Repositories
{
    public class EventRepository : IUserEventRepository<Event>
    {
        private SqlConnection _connection;

        public EventRepository()
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
        }

        public Event Create(Event entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CreateEvent";

            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Type", entity.Type);
            cmd.Parameters.AddWithValue("@Organizer", entity.Organizer);            
            cmd.Parameters.AddWithValue("@Date", entity.Date ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Location", entity.Location ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Tickets", entity.Tickets ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", entity.Price ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", entity.Description ?? (object)DBNull.Value);

            cmd.ExecuteNonQuery();
            return entity;
        }

        public IEnumerable<Event> GetAll()
        {
            List<Event> EventList = new List<Event>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllEvent";

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    EventList.Add(new Event
                    {
                        Id = (int)dr["Id"],
                        Name = (string)dr["Name"],
                        Type = (string)dr["Type"],
                        Organizer = (string)dr["Organizer"],                       
                        Date = (dr["Date"] is DBNull) ? null : (DateTime?)dr["Date"],
                        Location = (dr["Location"] is DBNull) ? null : (string)dr["Location"],
                        Tickets = (dr["Tickets"] is DBNull) ? null : (int?)dr["Tickets"],
                        Price = (dr["Price"] is DBNull) ? null : (double?)dr["Price"],
                        Description = (dr["Description"] is DBNull) ? null : (string)dr["Description"]
                    });
                }
            }
            return EventList;
        }

        public Event Get(int id)
        {
            Event e = new Event();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetEvent";

            cmd.Parameters.AddWithValue("@Id", id);
            using(SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    e.Id = (int)dr["Id"];
                    e.Name = (string)dr["Name"];
                    e.Type = (string)dr["Type"];
                    e.Organizer = (string)dr["Organizer"];                    
                    e.Date = (dr["Date"] is DBNull) ? null : (DateTime?)dr["Date"];
                    e.Location = (dr["Location"] is DBNull) ? null : (string)dr["Location"];
                    e.Tickets = (dr["Tickets"] is DBNull) ? null : (int?)dr["Tickets"];
                    e.Price = (dr["Price"] is DBNull) ? null : (double?)dr["Price"];
                    e.Description = (dr["Description"] is DBNull) ? null : (string)dr["Description"];
                }
            }
            return e;
        }

        public bool Update(int id, Event entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_UpdateEvent";

            cmd.Parameters.AddWithValue("@Name", entity.Name);
            cmd.Parameters.AddWithValue("@Type", entity.Type);
            cmd.Parameters.AddWithValue("@Organizer", entity.Organizer);            
            cmd.Parameters.AddWithValue("@Date", entity.Date ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Location", entity.Location ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Tickets", entity.Tickets ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Price", entity.Price ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Description", entity.Description ?? (object)DBNull.Value);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_DeleteEvent";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}
