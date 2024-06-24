﻿using ListOfItems.DataAccess;
using ListOfItems.Models;
using ListOfItems.Models.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ListOfItems.Controllers
{
    [Route("api/ItemListAPI")]
    [ApiController]
    public class ItemListAPIController : ControllerBase
    {
        public readonly DBConnect db;
        public ItemListAPIController(DBConnect db)
        {
            this.db = db;
        }
        [HttpGet]
        public ActionResult<IEnumerable<ItemList>> get()
        {

            var res = db.ItemMaster.Include(c => c.Category).Include(s => s.subCategory)
                      .Select(item => new ItemList
                      {
                          ItemId = item.ItemId,
                          CategoryId = item.Category.CategoryId,
                          
                          SubCategoryId = item.subCategory.SubCategoryId,
                         
                          ItemName = item.ItemName,
                          ItemDesc = item.ItemDesc,
                          Price = item.Price,
                          Discount = item.Discount
                      }).ToList();

            //var res1 = (from item in db.ItemMaster
            //           join c in db.Category_Master
            //           on item.CategoryId equals c.CategoryId
            //           join s in db.SubCategory_Master
            //           on item.SubCategoryId equals s.SubCategoryId
            //           select (new ItemList 
            //           {
            //               ItemId = item.ItemId,
            //               CategoryId = c.CategoryId,
            //               CategoryName = c.CategoryName,
            //               SubCategoryId = s.SubCategoryId,
            //               SubCategoryName = s.SubCategoryName,
            //               ItemName = item.ItemName,
            //               ItemDesc = item.ItemDesc,
            //               Price = item.Price,
            //               Discount = item.Discount
            //           })).ToList();
            if (res == null || res.Count<=0)
            {
                return NoContent();
            }
            else
            {
                return Ok(res.ToList());
            }

        }
        [HttpGet("{id:int}")]
        public ActionResult<ItemList> Get(int id)
        {
            try
            {
                var ress = db.ItemMaster.Include(C => C.Category).Include(S => S.subCategory).Where(u=>u.ItemId==id)
                          .Select(item => new ItemList
                          {
                              ItemId = item.ItemId,
                              CategoryId = item.Category.CategoryId,
                             
                              SubCategoryId = item.subCategory.SubCategoryId,
                              
                              ItemName = item.ItemName,
                              ItemDesc = item.ItemDesc,
                              Price = item.Price,
                              Discount = item.Discount
                          }).ToList();

                if (ress == null || ress.Count <= 0)
                {
                    return NoContent();
                }
                else
                {
                    return Ok(ress);
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public ActionResult<ItemListDTO> Create([FromBody] ItemListDTO itemdata) 
        {
            try
            {
                if(itemdata==null)
                {
                    return BadRequest();
                }
                else
                {
                    ItemList item = new ItemList
                    {
                        CategoryId = itemdata.CategoryId,
                        SubCategoryId = itemdata.SubCategoryId,
                        ItemName = itemdata.ItemName,
                        ItemDesc = itemdata.ItemDesc,
                        Price = itemdata.Price,
                        Discount = itemdata.Discount
                    };

                    db.ItemMaster.Add(item);
                    db.SaveChanges();
                    return Ok();
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
