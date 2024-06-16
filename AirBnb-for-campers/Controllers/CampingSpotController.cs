using AirBnb_for_campers.Data;
using Microsoft.AspNetCore.Mvc;
using AirBnb_for_campers.Models;


namespace AirBnb_for_campers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CampingSpotController : ControllerBase
    {
        private ICampingSpot campingspot_data;

        public CampingSpotController(ICampingSpot _campingspot_data)
        {
            campingspot_data = _campingspot_data;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CampingSpot>>> Get()
        {
            try
            {
                var spots = campingspot_data.GetCampingSpots();
                foreach (var spot in spots)
                {
                    if (!string.IsNullOrEmpty(spot.ImageUrl))
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", spot.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                            string contentType = GetContentType(filePath);
                            spot.ImageData = fileBytes;
                            spot.ImageContentType = contentType;
                        }
                    }
                }

                return Ok(spots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }

        private string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types.ContainsKey(ext) ? types[ext] : "application/octet-stream";
        }

        private Dictionary<string, string> GetMimeTypes()
        {
            return new Dictionary<string, string>
            {
                { ".jpg", "image/jpeg" },
                { ".jpeg", "image/jpeg" },
                { ".png", "image/png" },
                { ".gif", "image/gif" }
            };
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromForm] CampingSpot spot)
        {
            try
            {
                if (spot.Owner_Id == 0)
                {
                    return NotFound("Incorrect owner Id");
                }

                if (spot.ImageFile != null && IsImage(spot.ImageFile))
                {
                    // Generate a unique filename
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(spot.ImageFile.FileName);

                    // Save file to wwwroot/spotImages directory
                    var imagesPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "spotImages");
                    if (!Directory.Exists(imagesPath))
                    {
                        Directory.CreateDirectory(imagesPath);
                    }
                    var filePath = Path.Combine(imagesPath, fileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await spot.ImageFile.CopyToAsync(stream);
                    }

                    // Update image URL
                    spot.ImageUrl = $"/spotImages/{fileName}";

                    if (campingspot_data.Addcampingspot(spot))
                    {
                        return Ok(new { message = "Spot added successfully!", imageUrl = spot.ImageUrl });
                    }
                    else
                    {
                        // Rollback file saving if database update fails
                        System.IO.File.Delete(filePath);
                        return StatusCode(500, "An error occurred while storing the image.");
                    }
                }
                else
                {
                    if (campingspot_data.Addcampingspot(spot))
                    {
                        return Ok(new { message = "Spot added successfully!" });
                    }
                    else
                    {
                        return NotFound("Could not add spot");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest("Error adding spot: " + ex.Message);
            }
        }

        private bool IsImage(IFormFile file)
        {
            // Check if the uploaded file is an image
            var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            return allowedContentTypes.Contains(file.ContentType.ToLower());
        }

        [HttpGet("name")]
        public ActionResult<IEnumerable<CampingSpot>> GetByName(string name)
        {
            try
            {
                if (name != null) 
                    return Ok(campingspot_data.GetSpotByName(name));
                return NotFound("camping spot not found, try again!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("city")]
        public ActionResult<IEnumerable<CampingSpot>> GetByCity(string city)
        {
            try
            {
                if (city != null) 
                    return Ok(campingspot_data.GetSpotByCity(city));
                return NotFound("camping spot not found, try again!");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("description")]
        public ActionResult<IEnumerable<CampingSpot>> GetByDescription(string description)
        {
            try
            {
                if (description != null)
                    return Ok(campingspot_data.GetSpotByDescription(description));
                return NotFound("camping spot not found, try again!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("availability")]
        public ActionResult<IEnumerable<CampingSpot>> GetByAvailabilty(string available)
        {
            try
            {
                if (available != null)
                    return Ok(campingspot_data.GetSpotByAvailability(available));
                return NotFound("camping spot not found, try again!");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpGet("ownerId")]
        public async Task<ActionResult<IEnumerable<CampingSpot>>> GetCampingSpots(int id)
        {

            try
            {
                var spots = campingspot_data.GetOwnerCampingSpot(id);
                foreach (var spot in spots)
                {
                    if (!string.IsNullOrEmpty(spot.ImageUrl))
                    {
                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", spot.ImageUrl.TrimStart('/'));
                        if (System.IO.File.Exists(filePath))
                        {
                            var fileBytes = await System.IO.File.ReadAllBytesAsync(filePath);
                            string contentType = GetContentType(filePath);
                            spot.ImageData = fileBytes;
                            spot.ImageContentType = contentType;
                        }
                    }
                }

                return Ok(spots);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }



        }

        /*
         *** Authentication and Authorization ***
         * When an owner logs in, authenticate them and associate their owner_id with their authenticated session.
         * When an owner attempts to add a camping spot, retrieve the authenticated owner's owner_id from the session.
         * Compare the owner_id provided in the request with the authenticated owner's owner_id.
         * If they match, allow the camping spot to be added. If not, reject the reques.
         
         * This method (GetAuthenticatedOwnerId) to retrieve the authenticated owner's owner_id.
         * I need to implement this method based on an authentication mechanism 
         * (e.g., ASP.NET Identity, JWT authentication, etc.).
         * [HttpPost]
        public IActionResult AddCampingSpot([FromBody] CampingSpot campingSpot)
        {
            // Assuming you have a mechanism to authenticate the owner and associate their owner_id with their session
            int authenticatedOwnerId = GetAuthenticatedOwnerId(); // Example method to retrieve authenticated owner's owner_id

            // Verify that the owner_id provided in the request matches the authenticated owner's owner_id
            if (authenticatedOwnerId != campingSpot.Owner_Id)
            {
                return BadRequest("Invalid owner_id. You are not authorized to add camping spots for this owner.");
            }

            // Add your logic to insert the campingSpot into the database
            // Ensure to use campingSpot.Owner_Id when inserting into the database

            // Return success response
            return Ok("CampingSpot added successfully!");
        }*/

    }
}
