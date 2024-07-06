using Azure.Core.Serialization;
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
            if (UseDapper == false)
            {
                try
                {
                    using var conn = new SqlConnection(dbconnection);
                    var dynamicparameter = new DynamicParameters();
                    dynamicparameter.Add("@Flag", 1);

                    var res = conn.Query<ItemListDTO>("Usp_ItemList", dynamicparameter, commandTimeout: 500, commandType: CommandType.StoredProcedure).ToList();
                    return res;
                }
                catch
                {
                    throw;
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
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@Flag", 1);
                using SqlDataAdapter adap = new(cmd);
                DataTable dt = new();
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

        public ItemListDTO get(int id)
        {
            try
            {
                if (UseDapper)
                {
                    using var conn = new SqlConnection(dbconnection);
                    var dynamicpara = new DynamicParameters();
                    dynamicpara.Add("@Flag", 2);
                    dynamicpara.Add("@ItemId", id);
                    dynamicpara.Add("@OutputMessage", "", DbType.String, ParameterDirection.Output);
                    var res = conn.QueryFirstOrDefault("Usp_ItemList", dynamicpara, commandTimeout: 500, commandType: CommandType.StoredProcedure);
                    ItemListDTO result = new()
                    {
                        ItemId = res.ItemId,
                        ItemName = res.ItemName,
                        ItemDesc = res.ItemDesc,
                        CategoryId = res.CategoryId,
                        CategoryName = res.CategoryName,
                        SubCategoryId = res.SubCategoryId,
                        SubCategoryName = res.SubCategoryName,
                        Price = res.Price,
                        Discount = res.Discount,
                        ErrorMsg = dynamicpara.Get<string>("@OutputMessage")
                    };
                    return result;
                }
                else
                {
                    using var connection = new SqlConnection(dbconnection);
                    connection.Open();
                    using var cmd = new SqlCommand("Usp_ItemList", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 500;
                    cmd.Parameters.AddWithValue("@Flag", 2);
                    cmd.Parameters.AddWithValue("@ItemId", id);
                    DataSet ds = new DataSet();
                    using var adap = new SqlDataAdapter(cmd);
                    adap.Fill(ds);
                    return (from DataRow dr in ds.Tables[0].Rows
                            select new ItemListDTO()
                            {
                                ItemId = int.Parse(dr["ItemId"].ToString()),
                                CategoryId = int.Parse(dr["CategoryId"].ToString()),
                                CategoryName = dr["CategoryName"].ToString(),
                                SubCategoryId = int.Parse(dr["SubCategoryId"].ToString()),
                                SubCategoryName = dr["SubcategoryName"].ToString(),
                                ItemName = dr["ItemName"].ToString(),
                                ItemDesc = dr["ItemDesc"].ToString(),
                                Price = Convert.ToDecimal(dr["Price"].ToString()),
                                Discount = Convert.ToDecimal(dr["Discount"].ToString())

                            }).FirstOrDefault();

                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string Create(ItemListDTO item)
        {
            try
            {
               if(UseDapper)
                {
                    using var conn = new SqlConnection(dbconnection);
                    var dynamicpara = new DynamicParameters();
                    dynamicpara.Add("@Flag", 3);
                    dynamicpara.Add("@CategoryId", item.CategoryId);
                    dynamicpara.Add("@SubCategoryId", item.SubCategoryId);
                    dynamicpara.Add("@ItemName", item.ItemName);
                    dynamicpara.Add("@ItemDesc", item.ItemDesc);
                    dynamicpara.Add("@Price", item.Price);
                    dynamicpara.Add("@discount", item.Discount);
                    dynamicpara.Add("@OutputMessage", "", DbType.String, ParameterDirection.Output);

                    var res = conn.Execute("Usp_ItemList", dynamicpara, commandTimeout: 500,commandType:CommandType.StoredProcedure);
                    if(res==1)
                    {
                        return dynamicpara.Get<string>("@OutputMessage");
                    }
                    else
                    {
                        return res.ToString();
                    }
                }
                else
                {
                    using var conn = new SqlConnection(dbconnection);
                    conn.Open();
                    using var cmd = new SqlCommand("Usp_ItemList", conn);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout= 500;
                    cmd.Parameters.AddWithValue("@Flag", 3);
                    cmd.Parameters.AddWithValue("@CategoryId", item.CategoryId);
                    cmd.Parameters.AddWithValue("@SubCategoryId", item.SubCategoryId);
                    cmd.Parameters.AddWithValue("@ItemName", item.ItemName);
                    cmd.Parameters.AddWithValue("@ItemDesc", item.ItemDesc);
                    cmd.Parameters.AddWithValue("@Price", item.Price);
                    cmd.Parameters.AddWithValue("@discount", item.Discount);
                    using var adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);
                    if (ds.Tables[0].Columns.Contains("Msg"))
                    {
                        return ds.Tables[0].Rows[0]["Msg"].ToString();
                    }
                    else
                    {
                        return "Data Not Saved";
                    }
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        public string Delete(int id)
        {
            try
            {
                if(UseDapper)
                {
                    using var conn = new SqlConnection(dbconnection);
                    var dynamicpara = new DynamicParameters();
                    dynamicpara.Add("@Flag", 5);
                    dynamicpara.Add("@ItemId", id);
                    dynamicpara.Add("@OutputMessage", "", DbType.String, ParameterDirection.Output);

                    var res = conn.Execute("Usp_ItemList", dynamicpara, commandTimeout: 500, commandType: CommandType.StoredProcedure);
                    if(res!=1)
                    {
                        return dynamicpara.Get<string>("@OutputMessage");
                    }
                    else
                    {
                        return res.ToString();
                    }

                }
                else
                {
                    using var connection=new SqlConnection(dbconnection);
                    connection.Open();
                    using var cmd = new SqlCommand("Usp_ItemList", connection);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout=500;
                    cmd.Parameters.AddWithValue("@Flag", 5);
                    cmd.Parameters.AddWithValue("@ItemId", id);
                    SqlParameter sqlParameter = new SqlParameter() { ParameterName = "@OutputMessage", DbType = DbType.String, Size = 100, Direction = ParameterDirection.Output };
                    cmd.Parameters.Add(sqlParameter);
                    using var adap = new SqlDataAdapter(cmd);
                    DataSet ds = new DataSet();
                    adap.Fill(ds);

                    return sqlParameter.Value.ToString();
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public string Update(ItemListDTO item)
        {
            throw new NotImplementedException();
        }
    }
}
