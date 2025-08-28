using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using jwt.Dtos;

namespace jwt.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        [HttpPost]
        public ActionResult login(userData data)
        {
            //validate username and password
            if(data.username =="admin" && data.password == "123")
            {
                //generate token
                return Ok();
            }
            else
            {
                return Unauthorized();
            }
        }
    }
}
