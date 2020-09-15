using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TicketOnline.Forms;
using TicketOnline.Interfaces;
using TicketOnline.Models.Global;
using TicketOnline.Models.Global.Security;

namespace TicketOnlineApi.Repositories
{
    public class AuthRepository : IAuthenticateRepository<RegisterForm, LoginForm, User>
    {
        private SqlConnection _connection;
        private IUserEventRepository<User> _userRepository;
        private readonly AppSettings _appSettings;

        public AuthRepository(IOptions<AppSettings> app)
        {
            _connection = new SqlConnection(@"Data Source=FORMA-VDI1109\TFTIC;Initial Catalog=TicketOnline;User ID=sa;Password=tftic@2012;");
            _connection.Open();
            _userRepository = new UserRepository();
            _appSettings = app.Value;
        }

        public User Login(LoginForm loginForm)
        {
            User user = new User();
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_LoginUser";

            cmd.Parameters.AddWithValue("@Email", loginForm.Email);
            cmd.Parameters.AddWithValue("@Passwd", loginForm.Passwd);

            using (SqlDataReader dr = cmd.ExecuteReader())
            {
                while (dr.Read())
                {
                    user.Id = (int)dr["Id"];
                    //user.LastName = (dr["LastName"] is DBNull) ? null : (string)dr["LastName"];
                    //user.FirstName = (dr["FirstName"] is DBNull) ? null : (string)dr["FirstName"];
                    user.ScreenName = (string)dr["ScreenName"];
                    user.Email = (string)dr["Email"];
                    //user.Address = (dr["Address"] is DBNull) ? null : (string)dr["Address"];
                    //user.IsActive = (bool)dr["IsActive"];
                    //user.IsAdmin = (bool)dr["IsAdmin"];
                }
            }
            return user;
        }

        public void Register(RegisterForm registerForm)
        {
            SqlCommand cmd = _connection.CreateCommand();
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "SP_RegisterUser";

            cmd.Parameters.AddWithValue("@ScreenName", registerForm.ScreenName);
            cmd.Parameters.AddWithValue("@Email", registerForm.Email);
            cmd.Parameters.AddWithValue("@Passwd", registerForm.Passwd);

            cmd.ExecuteNonQuery();
        }

        public User Authenticate(User user)
        {
            //User user = _userRepository.GetAll().SingleOrDefault(u => u.Email == email && u.Passwd == passwd);

            if (user == null) return null;


            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            byte[] key = Encoding.ASCII.GetBytes(_appSettings.Secret);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Email, user.Id.ToString())
                }),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            SecurityToken token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user;
        }
    }
}
