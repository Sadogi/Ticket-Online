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
    public class CommentRepository : IBookingCommentRepository<Comment, GetComment>
    {
        private SqlConnection _connection;

        public CommentRepository()
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
        }

        public Comment Create(Comment entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CreateComment";

            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@EventId", entity.EventId);
            cmd.Parameters.AddWithValue("@Date", entity.Date = DateTime.Now);
            cmd.Parameters.AddWithValue("@Content", entity.Content);

            cmd.ExecuteNonQuery();
            return entity;
        }

        public IEnumerable<GetComment> GetAll(int eventId)
        {
            List<GetComment> CommentList = new List<GetComment>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllEventComment";

            cmd.Parameters.AddWithValue("@EventId", eventId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    CommentList.Add(new GetComment
                    {
                        Id = (int)dr["Id"],
                        UserId = (int)dr["UserId"],
                        EventId = (int)dr["EventId"],
                        Date = (DateTime)dr["Date"],
                        Content = (string)dr["Content"],
                        User = (string)dr["User"],
                        Event = (string)dr["Event"]
                    });
                }
            }
            return CommentList;
        }

        public IEnumerable<GetComment> GetAllByUser(int userId)
        {
            List<GetComment> CommentList = new List<GetComment>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllUserComment";

            cmd.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    CommentList.Add(new GetComment
                    {
                        Id = (int)dr["Id"],
                        UserId = (int)dr["UserId"],
                        EventId = (int)dr["EventId"],
                        Date = (DateTime)dr["Date"],
                        Content = (string)dr["Content"],
                        User = (string)dr["User"],
                        Event = (string)dr["Event"]
                    });
                }
            }
            return CommentList;
        }

        public GetComment Get(int id)
        {
            GetComment comment = new GetComment();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetComment";

            cmd.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    comment.Id = (int)dr["Id"];
                    comment.UserId = (int)dr["UserId"];
                    comment.EventId = (int)dr["EventId"];
                    comment.Date = (DateTime)dr["Date"];
                    comment.Content = (string)dr["Content"];
                    comment.User = (string)dr["User"];
                    comment.Event = (string)dr["Event"];
                }
            }
            return comment;
        }

        public bool Update(int id, Comment entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_UpdateComment";

            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@Content", entity.Content);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_DeleteComment";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}
