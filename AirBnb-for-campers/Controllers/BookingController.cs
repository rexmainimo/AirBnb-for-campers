using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;

namespace AirBnb_for_campers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController : ControllerBase
    {
        private IBooking Booking_Data;

        public BookingController(IBooking booking)
        {
            Booking_Data = booking;
        }

        [HttpPost("campingSpot")]
        public IActionResult IsBooked(Booking booking)
        {
   
            try
            {
                if (Booking_Data.BookCampingSpot(booking))
                {
                    return Ok(new { message = "Camping Spot booked successfully" });
                }
                else
                {
                    return BadRequest(new { message = "Booking failed!" });
                }
            }
            catch (Exception ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
        [HttpGet("user")]
        public async Task<ActionResult<Booking>> UserBookings (int id)
        {
            try
            {
                if (id == 0)
                {
                    return BadRequest(new { message = "Invalid id" });
                }

                var spots = Booking_Data.GetUserBookings(id);
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
                return BadRequest(new {message = "error: " + ex.Message});
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
        [HttpDelete("deleteBooking")]
        public IActionResult DeleteBooking(int booking_id)
        {
            try
            {
                if(booking_id == 0)
                {
                    return BadRequest();
                }
                if (Booking_Data.DeleteBooking(booking_id))
                {
                    return Ok();
                }
                else
                {
                    return NotFound();
                }
            }catch (Exception ex)
            {
                return BadRequest(new {message = ex.Message});
            }
        }
    }
}
