namespace Andreys.App.Controllers
{
    using Andreys.Services;
    using Andreys.ViewModels.Products;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class HomeController : Controller
    {
        private readonly IProductsService productsService;

        public HomeController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (this.IsUserLoggedIn())
            {
                var viewModel = new AllProductsViewModel
                {
                    Products = this.productsService.GetAllProducts(),
                };

                return this.View(viewModel, "Home");
            }

            return this.View();
        }
    }
}
