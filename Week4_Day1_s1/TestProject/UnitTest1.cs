using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;
using System.Reflection;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class ProductControllerTests
    {
         private const string ProductServiceName = "ProductService";
         private const string ProductControllerName = "ProductController";

        private HttpClient _httpClient;
        private Assembly _assembly;
        private Product _testProduct;

        [SetUp]
        public async Task Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API
            _assembly = Assembly.GetAssembly(typeof(dotnetapp.Services.ProductService));
            
            // Create a new test product before each test case
            _testProduct = await CreateTestProduct();
        }

        private async Task<Product> CreateTestProduct()
        {
            var newProduct = new Product
            {
                Id = 0, // Let the server assign the ID
                Name = "Test Product",
                Price = 9.99m,
                Description = "Test Product Description"
            };

            var json = JsonConvert.SerializeObject(newProduct);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/product", content);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Product>(await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task Test_GetAllProducts_ReturnsListOfProducts()
        {
            // Arrange - no specific arrangement needed as we're not modifying state
            // Act
            var response = await _httpClient.GetAsync("api/product");
            response.EnsureSuccessStatusCode();

            // Assert
            var content = await response.Content.ReadAsStringAsync();
            var products = JsonConvert.DeserializeObject<Product[]>(content);

            Assert.IsNotNull(products);
            Assert.IsTrue(products.Length > 0);
        }

        [Test]
        public async Task Test_GetProductById_ValidId_ReturnsProduct()
        {
            // Arrange - no specific arrangement needed as we're not modifying state
            // Act
            var response = await _httpClient.GetAsync($"api/product/{_testProduct.Id}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<Product>(content);

            Assert.IsNotNull(product);
            Assert.AreEqual(_testProduct.Id, product.Id);
        }
        [Test]
        public async Task Test_GetProductById_InvalidId_ReturnsNotFound()
        {
            var response = await _httpClient.GetAsync("api/product/999999"); // Using an invalid ID
            
            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public void Test_ProductService_Exist()
        {
            AssertServiceInstanceNotNull(ProductServiceName);
        }

        private void AssertServiceInstanceNotNull(string serviceName)
        {
            Type serviceType = _assembly.GetType($"dotnetapp.Services.{serviceName}");

            if (serviceType == null)
            {
                Assert.Fail($"Service {serviceName} does not exist.");
            }

            object serviceInstance = Activator.CreateInstance(serviceType);
            Assert.IsNotNull(serviceInstance);
        }
        [Test]
        public void Test_ProductController_Exist()
        {
            AssertControllerClassExist(ProductControllerName);
        }

        private void AssertControllerClassExist(string controllerName)
        {
            Type controllerType = _assembly.GetType($"dotnetapp.Controllers.{controllerName}");

            if (controllerType == null)
            {
                Assert.Fail($"Controller {controllerName} does not exist.");
            }
        }


        [TearDown]
        public async Task Cleanup()
        {
            _httpClient.Dispose();
        }

    }
}
