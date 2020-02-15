namespace Andreys.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Andreys.Data;
    using Andreys.Models;
    using Andreys.ViewModels.Products;

    public class ProductsService : IProductsService
    {
        private readonly AndreysDbContext db;

        public ProductsService(AndreysDbContext db)
        {
            this.db = db;
        }
        
        public void AddProduct(string name, string description, string imageUrl, decimal price, string category, string gender)
        {
            Enum.TryParse<Category>(category, out Category productCategory);
            Enum.TryParse<Gender>(gender, out Gender productGender);

            var newProduct = new Product()
            {
                Name = name,
                Description = description,
                ImageUrl = imageUrl,
                Price = price,
                Category = productCategory,
                Gender = productGender,
            };

            this.db.Products.Add(newProduct);
            this.db.SaveChanges();
        }

        public IEnumerable<ProductInfoViewModel> GetAllProducts()
        {
            return this.db.Products
                                  .Select(p => new ProductInfoViewModel()
                                  {
                                      Id = p.Id,
                                      Name = p.Name,
                                      Price = p.Price,
                                      ImageUrl = p.ImageUrl,
                                  })
                                  .ToArray();
        }

        public ProductDetailsViewModel GetProduct(string id)
        {
            var idAsInt = int.Parse(id);

            return this.db.Products
                          .Where(p => p.Id == idAsInt)
                          .Select(p => new ProductDetailsViewModel()
                          {
                              Id = p.Id,
                              Category = p.Category.ToString(),
                              Gender = p.Gender.ToString(),
                              Description = p.Description,
                              Name = p.Name,
                              Price = p.Price,
                              ImageUrl = p.ImageUrl,
                          })
                          .FirstOrDefault();
        }

        public void RemoveProduct(int id)
        {
            var product = this.db.Products.Find(id);
            this.db.Remove(product);
            this.db.SaveChanges();
        }
    }
}
