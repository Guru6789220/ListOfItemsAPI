using Dapper;
using ListOfItems.Models.DTO;
using ListOfItems.Services.IServices;
using Microsoft.Data.SqlClient;
using System.Data;

namespace ListOfItems.Services
{
    public class ItemListRepository : IItemRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string dbconnection;
        private readonly bool UseDapper = true;

        public ItemListRepository(IConfiguration _config)
        {
            _configuration = _config;
            dbconnection = _configuration.GetSection("ConnectionStrings").GetSection("ConnString").Value;
        }
        public List<ItemListDTO> getall()
        {
            if(UseDapper==false)
            {
                try
                {
                    using var conn=new SqlConnection(dbconnection);
                    var dynamicparameter = new DynamicParameters();
                    dynamicparameter.Add("@Flag", 1);

                    var res = conn.Query<ItemListDTO>("Usp_ItemList", dynamicparameter, commandTimeout: 500, commandType: CommandType.StoredProcedure).ToList();
                    return res;
                }
                catch
                {
                    throw ;
                }
                
            }
            else
            {
                using SqlConnection connection = new SqlConnection(dbconnection);
                connection.Open();
               // using SqlCommand cmd = new SqlCommand();
                using SqlCommand cmd = new("GetStudentDetails", connection);
                cmd.CommandText = "Usp_ItemList";
                cmd.CommandTimeout = 500;
                cmd.CommandType=CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                using SqlDataAdapter adap = new (cmd);
                DataTable dt = new ();
                //adap.SelectCommand = cmd;
                adap.Fill(dt);
                connection.Close();

                return (from DataRow dr in dt.Rows
                        select new ItemListDTO()
                        {
                            ItemId = Convert.ToInt32(dr["ItemId"]),
                            ItemName = dr["ItemName"].ToString(),
                            ItemDesc = dr["ItemDesc"].ToString(),
                            Price = Convert.ToDecimal(dr["Price"].ToString()),
                            Discount = Convert.ToDecimal(dr["Discount"].ToString()),
                            CategoryId = Convert.ToInt32(dr["CategoryId"].ToString()),
                            CategoryName = dr["CategoryName"].ToString(),
                            SubCategoryId = Convert.ToInt32(dr["SubCategoryId"].ToString()),
                            SubCategoryName = dr["SubCategoryName"].ToString()
                        }).ToList();
            }
        }
    }
}
