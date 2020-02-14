namespace IRunesApp.Controllers
{
    using IRunesApp.Services;
    using IRunesApp.ViewModels.Tracks;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class TracksController : Controller
    {
        private readonly ITracksService tracksService;

        public TracksController(ITracksService tracksService)
        {
            this.tracksService = tracksService;
        }

        public HttpResponse Create(string albumId)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new CreateTrackViewModel()
            {
                AlbumId = albumId,
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public HttpResponse Create(CreateTaskInputViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Redirect($"/Tracks/Create?albumId={input.AlbumId}");
            }

            if (string.IsNullOrWhiteSpace(input.Link))
            {
                return this.Redirect($"/Tracks/Create?albumId={input.AlbumId}");
            }

            if (input.Price < 0)
            {
                return this.Redirect($"/Tracks/Create?albumId={input.AlbumId}");
            }

            this.tracksService.CreateTrack(input.AlbumId, input.Name, input.Link, input.Price);
            return this.Redirect("/Albums/Details?id=" + input.AlbumId);
        }

        public HttpResponse Details(string albumId, string trackId)
        {
            var viewModel = this.tracksService.GetTrackDetails(trackId, albumId);
            return this.View(viewModel);
        }
    }
}
