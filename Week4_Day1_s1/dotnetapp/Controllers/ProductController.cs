using Microsoft.AspNetCore.Mvc;
using dotnetapp.Services;
using dotnetapp.Models;

using System.Collections.Generic;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        // GET api/product
        [HttpGet]
        public ActionResult<IEnumerable<Product>> GetAllProducts()
        {
            var products = _productService.GetAllProducts();
            return Ok(products);
        }

        // GET api/product/{productId}
        [HttpGet("{productId}")]
        public ActionResult<Product> GetProductById(int productId)
        {
            var product = _productService.GetProductById(productId);
            if (product == null)
            {
                return NotFound(); // HTTP 404
            }
            return Ok(product); // HTTP 200
        }

        // POST api/product
        [HttpPost]
        public ActionResult<Product> CreateProduct(Product newProduct)
        {
            if (newProduct == null)
            {
                return BadRequest(); // HTTP 400
            }
            _productService.AddProduct(newProduct);
            return CreatedAtAction(nameof(GetProductById), new { productId = newProduct.Id }, newProduct); // HTTP 201
        }
    }
}
