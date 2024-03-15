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
    public class BooksControllerTests
    {
        private const string BookServiceName = "BookService";
        private const string OrderServiceName = "OrderService";
        private const string BookRepositoryName = "BookRepository";
        private const string OrderRepositoryName = "OrderRepository";
        private HttpClient _httpClient;
        private Assembly _assembly;

        private Book _testBook;
        private Order _testOrder;


        [SetUp]
        public async Task Setup()
        {
            _assembly = Assembly.GetAssembly(typeof(dotnetapp.Services.IBookService));
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API

            // Create a new test book before each test case
            _testBook = await CreateTestBook();
            _testOrder = await CreateTestOrder();
        }

        private async Task<Book> CreateTestBook()
        {
            var newBook = new Book
            {
                BookId = 0, // Let the server assign the ID
                BookName = "Test Book",
                Category = "Test Category",
                Price = 10.99m
            };

            var json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/books", content);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task Test_GetAllBooks_ReturnsListOfBooks()
        {
            var response = await _httpClient.GetAsync("api/books");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<Book[]>(content);

            Assert.IsNotNull(books);
            Assert.IsTrue(books.Length > 0);
        }

        [Test]
        public async Task Test_GetBookById_ValidId_ReturnsBook()
        {
            var response = await _httpClient.GetAsync($"api/books/{_testBook.BookId}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);

            Assert.IsNotNull(book);
            Assert.AreEqual(_testBook.BookId, book.BookId);
        }

        [Test]
        public async Task Test_GetBookById_InvalidId_ReturnsNotFound()
        {
            var response = await _httpClient.GetAsync($"api/books/999");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Test_AddBook_ReturnsCreatedResponse()
        {
            var newBook = new Book
            {
                BookId = 0, // Let the server assign the ID
                BookName = "New Book",
                Category = "Fiction",
                Price = 19.99m
            };

            var json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/books", content);
            response.EnsureSuccessStatusCode();

            var createdBook = JsonConvert.DeserializeObject<Book>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdBook);
            Assert.AreEqual(newBook.BookName, createdBook.BookName);
        }

        [Test]
        public async Task Test_UpdateBook_ValidId_ReturnsNoContent()
        {
            _testBook.BookName = "Updated Book";

            var json = JsonConvert.SerializeObject(_testBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/books/{_testBook.BookId}", content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task Test_DeleteBook_ValidId_ReturnsNoContent()
        {
            var response = await _httpClient.DeleteAsync($"api/books/{_testBook.BookId}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
          private async Task<Order> CreateTestOrder()
        {
            var newOrder = new Order
            {
                OrderId = 0, // Let the server assign the ID
                CustomerName = "Test Customer",
                TotalAmount = 50.00m
            };

            var json = JsonConvert.SerializeObject(newOrder);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/orders", content);
            response.EnsureSuccessStatusCode();

            return JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());
        }

        [Test]
        public async Task Test_GetAllOrders_ReturnsListOfOrders()
        {
            var response = await _httpClient.GetAsync("api/orders");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<Order[]>(content);

            Assert.IsNotNull(orders);
            Assert.IsTrue(orders.Length > 0);
        }

        [Test]
        public async Task Test_GetOrderById_ValidId_ReturnsOrder()
        {
            var response = await _httpClient.GetAsync($"api/orders/{_testOrder.OrderId}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<Order>(content);

            Assert.IsNotNull(order);
            Assert.AreEqual(_testOrder.OrderId, order.OrderId);
        }

        [Test]
        public async Task Test_GetOrderById_InvalidId_ReturnsNotFound()
        {
            var response = await _httpClient.GetAsync($"api/orders/999");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task Test_AddOrder_ReturnsCreatedResponse()
        {
            var newOrder = new Order
            {
                OrderId = 0, // Let the server assign the ID
                CustomerName = "New Customer",
                TotalAmount = 100.00m
            };

            var json = JsonConvert.SerializeObject(newOrder);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/orders", content);
            response.EnsureSuccessStatusCode();

            var createdOrder = JsonConvert.DeserializeObject<Order>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdOrder);
            Assert.AreEqual(newOrder.CustomerName, createdOrder.CustomerName);
        }

        [Test]
        public async Task Test_UpdateOrder_ValidId_ReturnsNoContent()
        {
            _testOrder.CustomerName = "Updated Customer";

            var json = JsonConvert.SerializeObject(_testOrder);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/orders/{_testOrder.OrderId}", content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task Test_DeleteOrder_ValidId_ReturnsNoContent()
        {
            var response = await _httpClient.DeleteAsync($"api/orders/{_testOrder.OrderId}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }
        [Test]
        public void Test_BookService_Exist()
        {
            AssertServiceInstanceNotNull(BookServiceName);
        }

        [Test]
        public void Test_OrderService_Exist()
        {
            AssertServiceInstanceNotNull(OrderServiceName);
        }

        [Test]
        public void Test_BookRepository_Exist()
        {
            AssertRepositoryInstanceNotNull(BookRepositoryName);
        }

        [Test]
        public void Test_OrderRepository_Exist()
        {
            AssertRepositoryInstanceNotNull(OrderRepositoryName);
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

        private void AssertRepositoryInstanceNotNull(string repositoryName)
        {
            Type repositoryType = _assembly.GetType($"dotnetapp.Repository.{repositoryName}");

            if (repositoryType == null)
            {
                Assert.Fail($"Repository {repositoryName} does not exist.");
            }

            object repositoryInstance = Activator.CreateInstance(repositoryType);
            Assert.IsNotNull(repositoryInstance);
        }


        [TearDown]
        public async Task Cleanup()
        {
            // Delete the test book after each test case
            if (_testBook != null)
            {
                var response = await _httpClient.DeleteAsync($"api/books/{_testBook.BookId}");
                response.EnsureSuccessStatusCode();
            }
            if (_testOrder != null)
            {
                var response = await _httpClient.DeleteAsync($"api/orders/{_testOrder.OrderId}");
                response.EnsureSuccessStatusCode();
            }


            _httpClient.Dispose();
        }
    }
}
