namespace IRunesApp.Controllers
{
    using IRunesApp.Services;
    using IRunesApp.ViewModels.Albums;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class AlbumsController : Controller
    {
        private readonly IAlbumsService albumsService;

        public AlbumsController(IAlbumsService albumsService)
        {
            this.albumsService = albumsService;
        }

        public HttpResponse All()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = new AllAlbumsViewModel
            {
                Albums = this.albumsService.GetAll(),
            };

            return this.View(viewModel);
        }

        public HttpResponse Create()
        {
            return this.View();
        }

        [HttpPost]
        public HttpResponse Create(CreateAlbumInputViewModel input)
        {
            if (input.Name.Length < 4 || input.Name.Length > 20 ||
                string.IsNullOrWhiteSpace(input.Cover))
            {
                return this.Redirect("/Albums/Create");
            }

            this.albumsService.CreateAlbum(input.Name, input.Cover);
            return this.Redirect("/Albums/All");
        }

        public HttpResponse Details(string id)
        {
            var viewModel = this.albumsService.GetDetails(id);

            return this.View(viewModel);
        }
    }
}
