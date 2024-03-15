using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models;

namespace dotnetapp.Services
{
    public class ProductService
    {
        private static List<Product> _products = new List<Product>
        {
            new Product { Id = 1, Name = "Product 1", Price = 10.99m, Description = "Description 1" },
            new Product { Id = 2, Name = "Product 2", Price = 20.49m, Description = "Description 2" },
            new Product { Id = 3, Name = "Product 3", Price = 15.99m, Description = "Description 3" }
        };

        public IEnumerable<Product> GetAllProducts()
        {
            return _products;
        }

        public Product GetProductById(int productId)
        {
            return _products.FirstOrDefault(p => p.Id == productId);
        }

        public void AddProduct(Product newProduct)
        {
            // Assuming product IDs are unique and generated elsewhere
            _products.Add(newProduct);
        }
    }
}
