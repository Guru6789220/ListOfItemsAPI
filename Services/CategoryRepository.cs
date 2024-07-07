using Dapper;
using ListOfItems.Models.DTO;
using ListOfItems.Services.IServices;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ListOfItems.Services
{
    public class CategoryRepository:ICategoryRepository
    {
        private readonly IConfiguration configuration;
        private readonly string connectionstring;
        private readonly bool usedapper = true;

        public CategoryRepository(IConfiguration configuration)
        {
            this.configuration = configuration;
            connectionstring = configuration.GetSection("ConnectionStrings").GetSection("ConnString").Value;
        }

        public string Create(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }

        public string delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<CategoryDTO> Get()
        {
            using var conn=new SqlConnection(connectionstring);
            var dynamicpara = new DynamicParameters();
            dynamicpara.Add("@Flag", 1);

            var res = conn.Query<CategoryDTO>("Usp_Category", dynamicpara, commandType: CommandType.StoredProcedure, commandTimeout: 500).ToList();
            return res;
        }

        public CategoryDTO get(int id)
        {
            throw new NotImplementedException();
        }

        public string update(CategoryDTO categoryDTO)
        {
            throw new NotImplementedException();
        }
    }
}
