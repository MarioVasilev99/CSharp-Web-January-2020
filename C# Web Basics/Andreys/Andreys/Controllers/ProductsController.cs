namespace Andreys.Controllers
{
    using Andreys.Services;
    using Andreys.ViewModels.Products;
    using SIS.HTTP;
    using SIS.MvcFramework;

    public class ProductsController : Controller
    {
        private readonly IProductsService productsService;

        public ProductsController(IProductsService productsService)
        {
            this.productsService = productsService;
        }

        public HttpResponse Add()
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(AddProductInputViewModel input)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            if (input.Name.Length < 4 || input.Name.Length > 20)
            {
                return this.Redirect("/Products/Add");
            }

            if (string.IsNullOrWhiteSpace(input.Description) || input.Description.Length > 10)
            {
                return this.Redirect("/Products/Add");
            }

            if (string.IsNullOrWhiteSpace(input.ImageUrl))
            {
                return this.Redirect("/Products/Add");
            }

            if (input.Price <= 0)
            {
                return this.Redirect("/Products/Add");
            }

            this.productsService.AddProduct(
                input.Name,
                input.Description,
                input.ImageUrl,
                input.Price,
                input.Category,
                input.Gender);

            return this.Redirect("/");
        }

        public HttpResponse Details(string id)
        {
            if (!this.IsUserLoggedIn())
            {
                return this.Redirect("/Users/Login");
            }

            var viewModel = this.productsService.GetProduct(id);
            return this.View(viewModel);
        }

        public HttpResponse Delete(int id)
        {
            this.productsService.RemoveProduct(id);
            return this.Redirect("/");
        }
    }
}
