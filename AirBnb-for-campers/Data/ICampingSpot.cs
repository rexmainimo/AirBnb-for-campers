using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface ICampingSpot
    {
        bool Addcampingspot (CampingSpot spot);
        IEnumerable<CampingSpot> GetCampingSpots();
        IEnumerable<CampingSpot> GetSpotByName(string name);
        IEnumerable<CampingSpot> GetSpotByCity(string city);
        IEnumerable<CampingSpot> GetSpotByDescription(string name);
        IEnumerable<CampingSpot> GetSpotByAvailability(string name);
        IEnumerable<CampingSpot> GetOwnerCampingspot (int id);
    }
}
