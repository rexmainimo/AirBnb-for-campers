using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface ICampingSpot
    {
        bool Addcampingspot (CampingSpot spot);
        IEnumerable<CampingSpot> GetCampingSpots();
        IEnumerable<CampingSpot> GetSpotByName(string name);
        //CampingSpot GetCampingspotId (int id);*/
    }
}
