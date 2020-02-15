namespace Andreys.Services
{
    using System.Collections.Generic;

    using Andreys.ViewModels.Products;

    public interface IProductsService
    {
        void AddProduct(string name, string description, string imageUrl, decimal price, string category, string gender);

        IEnumerable<ProductInfoViewModel> GetAllProducts();

        ProductDetailsViewModel GetProduct(string id);

        void RemoveProduct(int id);
    }
}
