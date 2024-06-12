using AirBnb_for_campers.Models;

namespace AirBnb_for_campers.Data
{
    public interface IOwners
    {
        bool CreateNewOwner(Owner newOwner);

        int? OwnerLogin(OwnerLoginRequest loginRequest);
       
    }
}
