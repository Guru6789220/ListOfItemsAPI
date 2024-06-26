using ListOfItems.DataAccess;
using ListOfItems.Models;
using ListOfItems.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ListOfItems.Controllers
{
    [Route("api/CategoryAPI")]
    [ApiController]
    public class CategoryAPIController : ControllerBase
    {
        private readonly DBConnect db;
        public CategoryAPIController(DBConnect db)
        {
            this.db = db;
        }

        [HttpGet]
        public ActionResult<CategoryDTO> GET(int flag)
        {
            try
            {
                var messageParam = new SqlParameter
                {
                    ParameterName = "@Message",
                    SqlDbType = SqlDbType.NVarChar,
                    Size = 4000,
                    Direction = ParameterDirection.Output // This specifies it as an output parameter
                };
                var res = db.Category_Master.FromSql($"Usp_Category {flag},{0},{""},{""},{messageParam}").AsEnumerable().Select(c=>new CategoryDTO
                {
                    CategoryName= c.CategoryName,
                    CategoryId= c.CategoryId,
                    Avaliable=c.Avaliable
                }).ToList();
                //var res = db.Category_Master.Where(r=>r.Avaliable>0).Select(cm=>new CategoryDTO
                //{
                //    CategoryId=cm.CategoryId,
                //    CategoryName=cm.CategoryName,
                //    Avaliable=cm.Avaliable

                //}).ToList();
                if (res == null || res.ToList().Count <= 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(res);
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpGet("{id:int}")]
        public ActionResult<CategoryDTO> Get(int id,int flag)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var res = db.Category_Master.FromSql($"Usp_Category {flag},{id}").AsEnumerable().Select(c=>new CategoryDTO
            {
                CategoryId=c.CategoryId,CategoryName=c.CategoryName,Avaliable=c.Avaliable

            }).ToList();
            if(res==null)
            {
                return NoContent();
            }
            else
            {
                return Ok(res);
            }

        }

        [HttpPost]
        public ActionResult<CategoryDTO> Create([FromBody] CategoryDTO dto)
        {
            try
            {
                if (dto == null)
                {
                    return NoContent();
                }
                else
                {
                    Category category = new()
                    {
                        CategoryName = dto.CategoryName,
                        Avaliable = dto.Avaliable
                    };
                    db.Category_Master.Add(category);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        public ActionResult<CategoryDTO> Update([FromBody] CategoryDTO category)
        {
            if(category== null)
            {
                return StatusCode(204, "No Date To Update");
            }
            else
            {
                var res = db.Category_Master.FirstOrDefault(u => u.CategoryId == category.CategoryId);
                if(res==null)
                {
                    return NoContent();
                }
                else
                {
                    res.CategoryName = category.CategoryName;
                    res.Avaliable= category.Avaliable;

                    db.Category_Master.Update(res);
                    db.SaveChanges();
                    return Ok();
                }
            }
        }
        [HttpDelete("{id:int}")]
        public ActionResult<CategoryDTO> Delete(int id,int flag)
        {
            try
            {
                if (id == 0)
                {
                    return NoContent();
                }
                else
                {
                    var messageParam = new SqlParameter
                    {
                        ParameterName = "@Message",
                        SqlDbType = SqlDbType.NVarChar,
                        Size = 4000,
                        Direction = ParameterDirection.Output // This specifies it as an output parameter
                    };
                    var result = db.Database.ExecuteSql($"Usp_Category {flag},{id},{""},{""},{messageParam}");
                    var message = messageParam.Value.ToString();
                    if (result>0)
                    {
                        var mess = messageParam.Value.ToString();
                    }
                    
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }

    }
}
