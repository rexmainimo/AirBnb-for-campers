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
        public ActionResult<IEnumerable<CampingSpot>> Get()
        {
            try
            {
                return Ok(campingspot_data.GetCampingSpots());
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        public IActionResult Post([FromBody] CampingSpot spot)
        {
            try
            {
                campingspot_data.Addcampingspot(spot);

                return Ok("Spot added successfully!");
            }
            catch (Exception ex)
            {
                return BadRequest("Not added, problem in line 41:" + ex.Message);
                //throw new Exception(ex.Message);
            }
        }
        [HttpGet("{name}")]
        public ActionResult <IEnumerable<CampingSpot>> Get(string name)
        {
            if(name != null) return Ok(campingspot_data.GetSpotByName(name));
            return NotFound("camping spot not found, try again!");
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
