namespace SharedTrip.Services
{
    using SharedTrip.Models;
    using SharedTrip.ViewModels.Trips;

    public interface ITripsService
    {
        void AddTrip(AddTripInputViewModel input);

        AllTripsViewModel GetAllTrips();

        Trip GetTrip(string tripId);

        bool HasUserAlreadyJoined(string tripId, string userId);

        void AddUserToTrip(string tripId, string userId);
    }
}
