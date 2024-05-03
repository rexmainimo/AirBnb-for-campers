using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb_for_campers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private IUsers user_data;

        public UserController(IUsers newUserData) 
        { 
            user_data = newUserData;
        }

        [HttpPost]
        public IActionResult Post([FromBody] User newUser)
        {
            try
            {
                user_data.CreateNewUser(newUser);
                return Ok($"Sign up successful for: {newUser.FirstName}");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        [HttpGet]
        public IActionResult UserLogging(string username, string password)
        {
            try
            {
                if(user_data.Logging(username, password) == true)
                {
                    return Ok("Logging successful!");
                }
                return NotFound("Incorrect Username or Password");
                //return Ok(user_data.Logging(username, password) + " Logging successful!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
