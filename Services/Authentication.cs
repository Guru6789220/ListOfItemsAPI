using Azure.Core.Serialization;
using Dapper;
using ListOfItems.Models.DTO;
using ListOfItems.Services.IServices;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ListOfItems.Services
{
    public class Authentication : IAuthentiation
    {
        private readonly IConfiguration configuration;
        private readonly string connectionString;
        private readonly bool usedapper = true;

        public Authentication(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionString = configuration.GetSection("ConnectionStrings").GetSection("ConnString").Value;
        }

        public (string token,DateTime expires) GenertateToken(LoginDTO login)
        {
            try
            {
                var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                var credentails = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);


                var claims = new[]
                    {
                    new Claim(JwtRegisteredClaimNames.Name, login.UserName),
                    new Claim(JwtRegisteredClaimNames.Email, login.Emailid),
                    new Claim("Role", login.RoleType),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                    };
                var startdate = DateTime.Now;
                var expiration = DateTime.Now.AddMinutes(2);

                var token = new JwtSecurityToken(configuration["Jwt:Issuer"],
                    configuration["Jwt:Audience"],
                    claims,
                    expires: expiration,
                    signingCredentials: credentails);

                string tokens= new JwtSecurityTokenHandler().WriteToken(token);

                return (tokens, expiration);

            }
            catch (Exception ex)
            {
                return (ex.Message,DateTime.Now);
            }
        }

        public LoginDTO Login(LoginDTO login)
        {
            try
            {
                if (usedapper)
                {
                    using var conn = new SqlConnection(connectionString);
                    var dynamicpara = new DynamicParameters();
                    dynamicpara.Add("@Flag", 2);
                    dynamicpara.Add("@UserName", login.UserName);
                    dynamicpara.Add("@EmailId", login.UserName);
                    dynamicpara.Add("@OutputMessage", "", DbType.String, ParameterDirection.Output);

                    var res = conn.QueryFirstOrDefault<LoginDTO>("Usp_UserDetails", dynamicpara, commandType: CommandType.StoredProcedure, commandTimeout: 500);

                    //LoginDTO logindata = new LoginDTO();

                    //if(res!=null)
                    //{
                    //    logindata.UserName= res.UserName==null?res.Emailid:res.UserName;
                    //    logindata.Password = res.Password;
                    //    logindata.RoleType = res.RoleType;
                    //}
                    //else
                    //{
                    //    logindata.UserName = null;
                    //    logindata.Password = null;
                    //    logindata.RoleType = null;
                    //    logindata.ErrorMsg = dynamicpara.Get<string>("@OutputMessage").ToString();
                    //}


                    return res;

                }
                else
                {
                    using var connection = new SqlConnection(connectionString);
                    connection.Open();
                    using var cmd = new SqlCommand("Usp_UserDetails", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.AddWithValue("@Flag", 2);
                    cmd.Parameters.AddWithValue("@UserName", login.UserName);
                    cmd.Parameters.AddWithValue("@EmailId", login.UserName);
                    SqlParameter sqlpara = new SqlParameter() { ParameterName = "@OutputMessage", DbType = DbType.String, Size = 1000, Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(sqlpara);


                    DataSet ds = new DataSet();
                    using var adap = new SqlDataAdapter(cmd);
                    adap.Fill(ds);

                    LoginDTO logindata = new LoginDTO();

                    if (ds.Tables.Count > 0)
                    {
                        logindata.UserName = ds.Tables[0].Rows[0]["UserName"].ToString();
                        logindata.Emailid = ds.Tables[0].Rows[0]["EmailId"].ToString();
                        logindata.Password = ds.Tables[0].Rows[0]["Password"].ToString();
                        logindata.RoleType = ds.Tables[0].Rows[0]["RoleType"].ToString();
                    }
                    else
                    {
                        logindata = null;
                    }
                    return logindata;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
