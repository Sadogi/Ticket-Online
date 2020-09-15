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
    public class PaymentMethodRepository : IPaymentRepository<PaymentMethod, GetPaymentMethod>
    {
        private SqlConnection _connection;

        public PaymentMethodRepository()
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
        }

        public PaymentMethod Create(PaymentMethod entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_CreatePaymentMethod";

            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@CardHolder", entity.CardHolder);
            cmd.Parameters.AddWithValue("@CardNumber", entity.CardNumber);
            cmd.Parameters.AddWithValue("@ExpirationDate", entity.ExpirationDate);
            cmd.Parameters.AddWithValue("@CVVnumber", entity.CVVnumber);

            cmd.ExecuteNonQuery();
            return entity;
        }

        public IEnumerable<GetPaymentMethod> GetAll(int userId)
        {
            List<GetPaymentMethod> PaymentMethodList = new List<GetPaymentMethod>();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetAllPaymentMethod";

            cmd.Parameters.AddWithValue("@UserId", userId);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    PaymentMethodList.Add(new GetPaymentMethod
                    {
                        Id = (int)dr["Id"],
                        UserId = (int)dr["UserId"],
                        CardHolder = (string)dr["CardHolder"],
                        CardNumber = (string)dr["CardNumber"],
                        ExpirationDate = (DateTime)dr["ExpirationDate"],
                        CVVnumber = (int)dr["CVVnumber"],
                        User = (string)dr["User"],
                        Email = (string)dr["Email"]
                    });
                }
            }
            return PaymentMethodList;
        }

        public GetPaymentMethod Get(int id)
        {
            GetPaymentMethod paymentMethod = new GetPaymentMethod();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_GetPaymentMethod";

            cmd.Parameters.AddWithValue("@Id", id);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    paymentMethod.Id = (int)dr["Id"];
                    paymentMethod.UserId = (int)dr["UserId"];
                    paymentMethod.CardHolder = (string)dr["CardHolder"];
                    paymentMethod.CardNumber = (string)dr["CardNumber"];
                    paymentMethod.ExpirationDate = (DateTime)dr["ExpirationDate"];
                    paymentMethod.CVVnumber = (int)dr["CVVnumber"];
                    paymentMethod.User = (string)dr["User"];
                    paymentMethod.Email = (string)dr["Email"];
                }
            }
            return paymentMethod;
        }

        public bool Update(int id, PaymentMethod entity)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_UpdatePaymentMethod";

            cmd.Parameters.AddWithValue("@UserId", entity.UserId);
            cmd.Parameters.AddWithValue("@CardHolder", entity.CardHolder);
            cmd.Parameters.AddWithValue("@CardNumber", entity.CardNumber);
            cmd.Parameters.AddWithValue("@ExpirationDate", entity.ExpirationDate);
            cmd.Parameters.AddWithValue("@CVVnumber", entity.CVVnumber);
            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }

        public bool Delete(int id)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_DeletePaymentMethod";

            cmd.Parameters.AddWithValue("@Id", id);

            return cmd.ExecuteNonQuery() == 1;
        }
    }
}
