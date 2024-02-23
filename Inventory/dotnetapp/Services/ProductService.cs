using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.EntityFrameworkCore;

namespace dotnetapp.Services
{
    public class ProductService
    {
        private readonly ApplicationDbContext _context;

        public ProductService(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Product> GetAllProducts()
        {
            return _context.Products.Include(o => o.Orders).ToList();
        }

        // public Product GetProductById(int productId)
        // {
        //     return _context.Products.FirstOrDefault(p => p.ProductId == productId);
        // }

        public void AddProduct(Product product)
        {
            _context.Products.Add(product);
            _context.SaveChanges();
        }

        public void UpdateProduct(Product updatedProduct, int id)
        {
            var existingProduct = _context.Products.Find(id);

            if (existingProduct != null)
            {
                // Update properties based on your requirements
                existingProduct.Name = updatedProduct.Name;
                existingProduct.Description = updatedProduct.Description;
                existingProduct.Price = updatedProduct.Price;
                existingProduct.Quantity = updatedProduct.Quantity;

                _context.SaveChanges();
            }
        }

        public void DeleteProduct(int productId)
        {
            var productToDelete = _context.Products.Find(productId);

            if (productToDelete != null)
            {
                _context.Products.Remove(productToDelete);
                _context.SaveChanges();
            }
        }
    }
}
