using AirBnb_for_campers.Data;
using AirBnb_for_campers.Models;
using Microsoft.AspNetCore.Mvc;

namespace AirBnb_for_campers.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RatingsAndCommentsController : ControllerBase
    {
        private IRatingsAndComments ratings_comments;

        public RatingsAndCommentsController(IRatingsAndComments _ratings_comments)
        {
            ratings_comments = _ratings_comments;
        }
        [HttpPost("ratings")]
        public IActionResult AddRatingsAndComments(RatingsAndComments content)
        {
            try
            {
                if (ratings_comments.IsRatingOrComment(content))
                {
                    return Ok(new { message = "Reaction added successfully." });
                }
                return BadRequest(new { message = "Failed to add reaction" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public ActionResult<IEnumerable<RateAndCommentInfo>> AllRatingsAndComments()
        {
            try
            {
                return Ok(new { message = ratings_comments.GetRatingsAndComments() });
            }
            catch (Exception ex)
            {
                return NotFound(new {message =  ex.Message});
            }
        }
    }
}
