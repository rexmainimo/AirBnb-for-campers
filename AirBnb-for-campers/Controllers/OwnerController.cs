using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb_for_campers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OwnerController : ControllerBase
    {
        private IOwners owner_data;
        public OwnerController(IOwners newOwnerData)
        {
            owner_data = newOwnerData;
        }

        [HttpPost("registration")]
        public IActionResult Post([FromBody] Owner newOwner)
        {
            try
            {
                owner_data.CreateNewOwner(newOwner);
                    return Ok(new { message = "Account created successfully." });

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("login")]
        public IActionResult OwnerLogin([FromBody] OwnerLoginRequest ownerLoginRequest)
        {
            try
            {
                if (ownerLoginRequest == null || ownerLoginRequest.Id == null ||
                string.IsNullOrEmpty(ownerLoginRequest.Password))
                {
                    return BadRequest(new { message = "Id and Password are required!" });
                }
                int? ownerId = owner_data.OwnerLogin(ownerLoginRequest);
                if (ownerId.HasValue)
                {
                    return Ok(new { message = "Login successful.", ownerId.Value });
                }
                else
                {
                    return NotFound(new { message = "Invalid Username or Password" });
                }
            }
            catch(Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while logging in.", error = ex.Message });
            }

        }
    }
}
