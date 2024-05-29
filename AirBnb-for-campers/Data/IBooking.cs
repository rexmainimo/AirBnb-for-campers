using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IBooking
    {
        bool BookCampingSpot(Booking booking);
        IEnumerable<BookingInfo> GetUserBookings(int id);
    }
}
