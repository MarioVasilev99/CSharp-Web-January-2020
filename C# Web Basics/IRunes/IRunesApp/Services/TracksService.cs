namespace IRunesApp.Services
{
    using IRunesApp.Models;
    using IRunesApp.ViewModels.Tracks;
    using System.Linq;

    public class TracksService : ITracksService
    {
        private readonly ApplicationDbContext db;

        public TracksService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateTrack(string albumId, string name, string link, decimal price)
        {
            var newTrack = new Track
            {
                AlbumId = albumId,
                Name = name,
                Link = link,
                Price = price,
            };

            this.db.Tracks.Add(newTrack);

            var allTrackPricesSum = this.db.Tracks
                .Where(t => t.AlbumId == albumId)
                .Sum(t => t.Price) + price;

            var album = this.db.Albums.FirstOrDefault(a => a.Id == albumId);
            album.Price = allTrackPricesSum * 0.87M;

            this.db.SaveChanges();
        }

        public TrackDetailsViewModel GetTrackDetails(string trackId, string albumId)
        {
            var trackViewModel = db.Tracks
                .Where(t => t.Id == trackId && t.AlbumId == albumId)
                .Select(t => new TrackDetailsViewModel
                {
                    Name = t.Name,
                    Link = t.Link,
                    Price = t.Price,
                    AlbumId = t.AlbumId,
                })
                .FirstOrDefault();

            return trackViewModel;
        }
    }
}
