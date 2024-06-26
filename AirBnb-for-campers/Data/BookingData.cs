﻿using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public class BookingData : IBooking
    {
        private readonly Database db = new Database();

        public bool BookCampingSpot(Booking booking)
        {
            if(booking.User_id != 0 || booking.CampinpingSpot_id != 0)
            {
                string query = "INSERT INTO `Bookings` (`BookingDate`, `StartDate`, `EndDate`, `NumOfPeople`, `Price`, `User_id`, `CampingSpot_id`) " +
                    "VALUES (@BookingDate, @StartDate, @EndDate, @NumOfPeople, @Price, @User_id, @CampingSpot_id)";

                Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    {"@BookingDate", booking.BookingDate },
                    {"@StartDate", booking.StartDate },
                    {"@EndDate", booking.EndDate },
                    {"@NumOfPeople", booking.NumOfPeople },
                    {"@Price", booking.Price },
                    {"@User_id", booking.User_id },
                    {"@CampingSpot_id", booking.CampinpingSpot_id }
                };

                if(db.ExecuteQuery(query, parameters))
                {
                    return true;
                }
            }
            return false;
        }
        public IEnumerable<BookingInfo> GetUserBookings(int id)
        {
            string query = "SELECT Booking_id, BookingDate, StartDate, EndDate, NumOfPeople, Price, " +
                   "campingspots.Name, campingspots.City, campingspots.PostalNum, campingspots.StreetNum, " +
                   "campingspots.HouseNum, campingspots.Description, campingspots.Facilities, campingspots.Image " +
                   "FROM bookings " +
                   "INNER JOIN campingspots ON bookings.CampingSpot_id = campingspots.CampingSpot_id " +
                   "WHERE User_id = @UserId";
            Dictionary<string, object> parameters = new Dictionary<string, object>
                {
                    { "@UserId", id }
                };
            return db.ExtractUserBookings(query, parameters);
        }
        public bool DeleteBooking(int id)
        {
            string query = "DELETE FROM `Bookings` WHERE `Booking_id` = @Booking_id";
            Dictionary<string, object> parameter = new Dictionary<string, object>
            {
                {"@Booking_id", id }
            };
            return db.ExecuteQuery(query, parameter);
        }
        
    }
}
