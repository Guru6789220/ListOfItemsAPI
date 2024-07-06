using ListOfItems.DataAccess;
using ListOfItems.Models;
using ListOfItems.Models.DTO;
using ListOfItems.Services.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ListOfItems.Controllers
{
    [Route("api/ItemListAPI")]
    [ApiController]
    public class ItemListAPIController : ControllerBase
    {
       
        private readonly IItemRepository itemRepository;
        public ItemListAPIController(IItemRepository ItemReop)
        {
            
            itemRepository = ItemReop;
        }
        [HttpGet]
        public async Task<List<ItemListDTO>> Get()
        {
            var res= await Task.FromResult(itemRepository.getall());
            return res;

        }
     
        [HttpGet("{id:int}")]
        public ActionResult<ItemList> Get(int id)
        {
            try
            {
                var ress = itemRepository.get(id);

                if (ress == null)
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
                    var result = itemRepository.Create(itemdata);
                    return Ok(result);
                }
            }
            catch(Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0)
                {
                    return NoContent();
                }
                else
                {
                    var delete = itemRepository.Delete(id);
                    return Ok(delete);

                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
