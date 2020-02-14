using System.Collections.Generic;
using System.Linq;
using IRunesApp.Models;
using IRunesApp.ViewModels.Albums;

namespace IRunesApp.Services
{
    public class AlbumsService : IAlbumsService
    {
        private readonly ApplicationDbContext db;

        public AlbumsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public void CreateAlbum(string name, string cover)
        {
            var newAlbum = new Album()
            {
                Name = name,
                Cover = cover,
                Price = 0.0M,
            };

            this.db.Albums.Add(newAlbum);
            this.db.SaveChanges();
        }

        public IEnumerable<AlbumInfoViewModel> GetAll()
        {
            var albums = this.db.Albums
                .Select(a => new AlbumInfoViewModel
                {
                    Id = a.Id,
                    Name = a.Name
                })
                .ToArray();

            return albums;
        }

        public AlbumDetailsViewModel GetDetails(string id)
        {
            var albumDetails = this.db.Albums
                .Where(a => a.Id == id)
                .Select(a => new AlbumDetailsViewModel()
                {
                    Id = a.Id,
                    Cover = a.Cover,
                    Name = a.Name,
                    Price = a.Price,
                    Tracks = a.Tracks
                              .Select(t => new TrackInfoViewModel()
                              {
                                  Id = t.Id,
                                  Name = t.Name,
                              })
                }).FirstOrDefault();

            return albumDetails;
        }
    }
}
