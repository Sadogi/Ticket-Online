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
    public class BookingRepository : IBookingCommentRepository<Booking, GetBooking>
    {
        private SqlConnection _connection;

        public BookingRepository()
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
        }

        public Booking Create(Booking entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CreateBooking";

            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@PurchaseDate", entity.PurchaseDate = DateTime.Now);
            cmd.Parameters.AddWithValue("@TicketsPurchased", entity.TicketsPurchased);
            cmd.Parameters.AddWithValue("@Amount", entity.Amount);

            cmd.ExecuteNonQuery();
            return entity;
        }

        public IEnumerable<GetBooking> GetAll(int eventId)
        {
            List<GetBooking> BookingList = new List<GetBooking>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllEventBooking";

            cmd.Parameters.AddWithValue("@EventId", eventId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    BookingList.Add(new GetBooking
                    {
                        Id = (int)dr["Id"],
                        UserId = (int)dr["UserId"],
                        EventId = (int)dr["EventId"],
                        PurchaseDate = (DateTime)dr["PurchaseDate"],
                        TicketsPurchased = (int)dr["TicketsPurchased"],
                        Amount = (double)dr["Amount"],                       
                        User = (string)dr["User"],                        
                        Email = (string)dr["Email"],
                        Event = (string)dr["Event"]
                    });
                }
            }
            return BookingList;
        }

        public IEnumerable<GetBooking> GetAllByUser(int userId)
        {
            List<GetBooking> BookingList = new List<GetBooking>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllUserBooking";

            cmd.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    BookingList.Add(new GetBooking
                    {
                        Id = (int)dr["Id"],
                        UserId = (int)dr["UserId"],
                        EventId = (int)dr["EventId"],
                        PurchaseDate = (DateTime)dr["PurchaseDate"],
                        TicketsPurchased = (int)dr["TicketsPurchased"],
                        Amount = (double)dr["Amount"],
                        User = (string)dr["User"],
                        Email = (string)dr["Email"],
                        Event = (string)dr["Event"]                        
                    });
                }
            }
            return BookingList;
        }

        public GetBooking Get(int id)
        {
            GetBooking booking = new GetBooking();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetBooking";

            cmd.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    booking.Id = (int)dr["Id"];
                    booking.UserId = (int)dr["UserId"];
                    booking.EventId = (int)dr["EventId"];
                    booking.PurchaseDate = (DateTime)dr["PurchaseDate"];
                    booking.TicketsPurchased = (int)dr["TicketsPurchased"];
                    booking.Amount = (double)dr["Amount"];                 
                    booking.User = (string)dr["User"];
                    booking.Email = (string)dr["Email"];
                    booking.Event = (string)dr["Event"];                    
                }
            }
            return booking;
        }

        public bool Update(int id, Booking entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_UpdateBooking";

            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@PurchaseDate", entity.PurchaseDate);
            cmd.Parameters.AddWithValue("@TicketsPurchased", entity.TicketsPurchased);
            cmd.Parameters.AddWithValue("@Amount", entity.Amount);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_DeleteBooking";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}
