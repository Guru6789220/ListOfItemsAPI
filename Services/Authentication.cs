using Dapper;
using ListOfItems.Models.DTO;
using ListOfItems.Services.IServices;
using Microsoft.Data.SqlClient;

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
        }

        public LoginDTO Login(LoginDTO login)
        {
            throw new NotImplementedException();
        }
        //public LoginDTO Login(LoginDTO login)
        //{
        //    try
        //    {
        //        if(usedapper)
        //        {
        //            using var conn = new SqlConnection(connectionString);
        //            var dynamicpara = new DynamicParameters();


        //        }
        //        else
        //        {

        //        }

        //    }
        //    catch(Exception ex)
        //    {
        //        throw ex;
        //    }
        //}
    }
}
