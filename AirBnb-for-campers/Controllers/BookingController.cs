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
        public ActionResult <Booking> UserBookings (int id)
        {
            try
            {
                if(id == 0)
                {
                    return BadRequest(new { message = "Invalid id" });
                }
                return Ok(Booking_Data.GetUserBookings(id));
            }
            catch (Exception ex)
            {
                return BadRequest(new {message = "error: " + ex.Message});
            }
        }
    }
}
