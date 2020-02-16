namespace SharedTrip.Services
{
    using System;
    using System.Linq;
    using SharedTrip.Models;
    using SharedTrip.ViewModels.Trips;

    public class TripsService : ITripsService
    {
        private readonly ApplicationDbContext db;

        public TripsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void AddTrip(AddTripInputViewModel input)
        {
            var newTrip = new Trip()
            {
                Id = Guid.NewGuid().ToString(),
                StartPoint = input.StartPoint,
                EndPoint = input.EndPoint,
                DepartureTime = input.DepartureTime,
                ImagePath = input.ImagePath,
                Seats = input.Seats,
                Description = input.Description,
            };

            this.db.Trips.Add(newTrip);
            this.db.SaveChanges();
        }

        public void AddUserToTrip(string tripId, string userId)
        {
            var user = this.db.Users.Find(userId);
            user.UserTrips.Add(new UserTrip()
            {
                TripId = tripId,
                UserId = userId,
            });
            
            this.db.SaveChanges();
        }

        public AllTripsViewModel GetAllTrips()
        {
            var trips = this.db.Trips
                .Select(t => new TripDetailsHomeViewModel()
                {
                    TripId = t.Id,
                    Seats = t.Seats,
                    DepartureTime = t.DepartureTime.ToString("dd.MM.yyyy HH:mm"),
                    StartPoint = t.StartPoint,
                    EndPoint = t.EndPoint,
                }).ToArray();

            var viewModel = new AllTripsViewModel()
            {
                Trips = trips,
            };

            return viewModel;
        }

        public Trip GetTrip(string tripId) =>
            this.db.Trips.FirstOrDefault(t => t.Id == tripId);

        public bool HasUserAlreadyJoined(string tripId, string userId)
        {
            return this.db.UserTrips.Any(t => t.TripId == tripId && t.UserId == userId);
        }
    }
}
