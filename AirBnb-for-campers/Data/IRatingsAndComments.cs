using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IRatingsAndComments
    {
        bool IsRatingOrComment(RatingsAndComments ratingsAndComments);
        IEnumerable<RateAndCommentInfo> GetRatingsAndComments();
    }
}
