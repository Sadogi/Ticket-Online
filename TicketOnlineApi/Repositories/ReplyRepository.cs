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
    public class ReplyRepository : IBookingCommentRepository<Reply, GetReply>
    {
        private SqlConnection _connection;

        public ReplyRepository()
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
        }

        public Reply Create(Reply entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CreateReply";

            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@CommentId", entity.CommentId);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@Content", entity.Content);

            cmd.ExecuteNonQuery();
            return entity;
        }

        public IEnumerable<GetReply> GetAll(int commentId)
        {
            List<GetReply> ReplyList = new List<GetReply>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllCommentReply";

            cmd.Parameters.AddWithValue("@CommentId", commentId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    ReplyList.Add(new GetReply
                    {
                        Id = (int)dr["Id"],
                        UserId = (int)dr["UserId"],
                        CommentId = (int)dr["CommentId"],
                        Date = (DateTime)dr["Date"],
                        Content = (string)dr["Content"],
                        User = (string)dr["User"],
                        Comment = (string)dr["Comment"]
                    });
                }
            }
            return ReplyList;
        }

        public IEnumerable<GetReply> GetAllByUser(int userId)
        {
            List<GetReply> ReplyList = new List<GetReply>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllUserReply";

            cmd.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    ReplyList.Add(new GetReply
                    {
                        Id = (int)dr["Id"],
                        UserId = (int)dr["UserId"],
                        CommentId = (int)dr["CommentId"],
                        Date = (DateTime)dr["Date"],
                        Content = (string)dr["Content"],
                        User = (string)dr["User"],
                        Comment = (string)dr["Comment"]
                    });
                }
            }
            return ReplyList;
        }

        public GetReply Get(int id)
        {
            GetReply reply = new GetReply();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetReply";

            cmd.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    reply.Id = (int)dr["Id"];
                    reply.UserId = (int)dr["UserId"];
                    reply.CommentId = (int)dr["CommentId"];
                    reply.Date = (DateTime)dr["Date"];
                    reply.Content = (string)dr["Content"];
                    reply.User = (string)dr["User"];
                    reply.Comment = (string)dr["Comment"];
                }
            }
            return reply;
        }

        public bool Update(int id, Reply entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_UpdateReply";

            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@CommentId", entity.CommentId);
            cmd.Parameters.AddWithValue("@Date", entity.Date);
            cmd.Parameters.AddWithValue("@Content", entity.Content);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_DeleteReply";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}
