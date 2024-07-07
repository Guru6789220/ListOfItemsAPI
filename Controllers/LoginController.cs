using ListOfItems.Models.DTO;
using ListOfItems.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace ListOfItems.Controllers
{
    [Route("api/Login")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthentiation authentiation;
        public LoginController(IAuthentiation Authe)
        {
            authentiation= Authe;
        }

        [HttpPost]
        public ActionResult<LoginDTO> Login([FromBody] LoginDTO loginDTO)
        {
            try
            {
                if (loginDTO == null)
                {
                    return BadRequest();
                }
                else
                {
                    var res = authentiation.Login(loginDTO);
                    if (res != null)
                    {
                        if((loginDTO.UserName==res.UserName || loginDTO.UserName==res.Emailid)&& loginDTO.Password==res.Password)
                        {
                           (string token,DateTime expires)= authentiation.GenertateToken(res);
                            return Ok(token);
                        }
                        else
                        {
                            return StatusCode(400, "Invalid UserName Or Password");
                        }
                    }
                    else
                    {
                        return StatusCode(400, "Invalid UserName Or Password");
                    }
                }
            }
            catch (Exception ee)
            {
                return BadRequest(ee.Message);
            }
        }

    }
}
