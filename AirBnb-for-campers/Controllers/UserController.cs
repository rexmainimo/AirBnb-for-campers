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

        [HttpPost("registration")]
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

        [HttpPost("login")]
        public IActionResult UserLogging([FromBody] LoginRequest loginRequest)
        {
            try
            {
                if (string.IsNullOrEmpty(loginRequest.Username) || string.IsNullOrEmpty(loginRequest.Password))
                {
                    return BadRequest(new { message = "Username and Password are required!" });
                }
                bool isverified = user_data.Logging(loginRequest);
                if(isverified)
                {
                    return Ok(new { message = "Logging successful!" });
                }
                else
                {
                    return NotFound(new { message = "Invalid Username or Password" });
                }
                
                
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("uploadProfilePicture")]
        
        public async Task<IActionResult> UploadProfilePicture(IFormFile file, int userId)
        {
            if (file == null || file.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            var filePath = Path.Combine("wwwroot", "images", file.FileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            string profilePictureUrl = $"/images/{file.FileName}";
            if (user_data.UploadProfilePicture(userId, profilePictureUrl))
            {
                return Ok(new { Message = "Profile picture updated successfully.", ProfilePictureUrl = profilePictureUrl });
            }
            return StatusCode(500, "An error occurred while updating the profile picture.");
        }


    }
}
