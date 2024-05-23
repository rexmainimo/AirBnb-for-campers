using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Linq;

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
                return Ok(new { message = $"Sign up successful for: {newUser.FirstName}" });
            }
            catch (Exception ex)
            {
                return BadRequest(new{message = ex.Message});
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
                int? userId = user_data.Logging(loginRequest);

                if(userId.HasValue)
                {
                    return Ok(new { message = userId.Value });
                }
                else
                {
                    return NotFound(new { message = "Invalid Username or Password" });
                }
                
                
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while logging in.", error = ex.Message });
            }
        }
        [HttpGet("{Id}")]
        public ActionResult <IEnumerable<User>>  RetrieveUserInfor(int userId)
        {
            try
            {
                if (userId == null || userId == 0) 
                {
                    return BadRequest(new { message = "Insert valid User Id" });
                }
                return Ok(user_data.GetUserInfo(userId));
            }
            catch(Exception ex)
            {
                return StatusCode(500, new {message = "Error retrieving user information.", error = ex.Message});
            }
        } 
        [HttpPut("updateUserInfo")]
        public IActionResult UpdateUserInformation([FromBody] User user)
        {
            if (user == null)
            {
                return BadRequest(new { message = "User data is null or invalid" });
            }

            try
            {
                bool isUpdated = user_data.UpdateUserInfo(user);

                if (isUpdated)
                {
                    return Ok(new { message = "User information updated successfully", user });
                }
                else
                {
                    return NotFound(new { message = "User information not updated" });
                }
            }
            catch (Exception ex)
            {
                // Log the exception here (e.g., using a logging framework)
                return StatusCode(500, new { message = "Internal server error", error = ex.Message });
            }
        }

        // incomplete, still needs to return the image posted by the user back to the frontend!
        [HttpPost("uploadProfilePicture")]
        public async Task<IActionResult> UploadProfilePicture(IFormFile profilePicture, int userId)
        {
            if (profilePicture == null || profilePicture.Length == 0)
            {
                return BadRequest("No file uploaded.");
            }

            try
            {
                // Validate file type
                if (!IsImage(profilePicture))
                {
                    return BadRequest(new { message = "Invalid file type. Only images are allowed." });
                }

                // Generate a unique filename
                string fileName = Guid.NewGuid().ToString() + Path.GetExtension(profilePicture.FileName);

                // Save file to wwwroot/images directory
                var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                if (!Directory.Exists(imagesPath))
                {
                    Directory.CreateDirectory(imagesPath);
                }

                var filePath = Path.Combine(imagesPath, fileName);
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await profilePicture.CopyToAsync(stream);
                }

                // Update profile picture URL in the database
                string profilePictureUrl = $"/images/{fileName}";
                if(user_data.UploadProfilePictureToDb(userId, profilePictureUrl) == true)
                {
                    return Ok(new { message = "Profile picture updated successfully.", ProfilePictureUrl = profilePictureUrl });
                    //Ok(new { message = (IActionResult)profilePicture });//
                }
                else
                {
                    // Rollback file saving if database update fails
                    System.IO.File.Delete(filePath);
                    return StatusCode(500, "An error occurred while updating the profile picture.");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
        private bool IsImage(IFormFile file)
        {
            // Check if the uploaded file is an image
            if (file.ContentType.ToLower() == "image/jpeg" ||
                file.ContentType.ToLower() == "image/png" ||
                file.ContentType.ToLower() == "image/gif")
            {
                return true;
            }
            return false;
        }
        [HttpDelete("deleteProfilePicture")]
        public IActionResult DeleteProfilePicture(int userId)
        {
            // Get the current profile picture URL from the database
            string profilePictureUrl = user_data.GetProfilePictureUrl(userId);
            if (string.IsNullOrEmpty(profilePictureUrl))
            {
                return NotFound("Profile picture not found.");
            }

            // Delete the profile picture from the server
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", profilePictureUrl.TrimStart('/'));
            if(System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            else
            {
                return NotFound("Profile Picture file not found.");
            }

            // Update the database to remove the profile picture URL
            if (user_data.DeleteProfilePicture(userId))
            {
                return Ok("Profile picture deleted succesfully.");
            }
            return StatusCode(500, "An error occured while deleting the profile picture.");
        }
    }
}
