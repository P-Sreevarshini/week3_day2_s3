// using NUnit.Framework;
// using System;
// using dotnetapp.Models;
// using Microsoft.AspNetCore.Mvc;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.Extensions.DependencyInjection;
// using System.IO;



// namespace dotnetapp.Tests
// {
//     [TestFixture]
//     public class ControllerTests
//     {
//         private string _booksControllerName;
//         private string _ordersControllerName;
//         private string _bookServiceName;
//         private string _orderServiceName;
//         private string _bookRepositoryName;
//         private string _orderRepositoryName;

//         [SetUp]
//         public void Setup()
//         {
//             _bookRepositoryName = "BookRepository";
//             _orderRepositoryName = "OrderRepository";
//             _bookServiceName = "BookService";
//             _orderServiceName = "OrderService";
//             _booksControllerName = "BooksController";
//             _ordersControllerName = "OrdersController";
//         }

//         [Test]
//         public void Get_all_Books_Returns_Ok_Result()
//         {
//             // Arrange
//             var booksController = GetControllerInstance(_booksControllerName, _bookServiceName);

//             // Act
//             var result = GetMethodResult(booksController, "GetAllBooks");

//             // Assert
//             Assert.AreEqual("OkObjectResult", result);
//         }

//         [Test]
//         public void Get_all_Orders_Returns_Ok_Result()
//         {
//             // Arrange
//             var ordersController = GetControllerInstance(_ordersControllerName, _orderServiceName);

//             // Act
//             var result = GetMethodResult(ordersController, "GetAllOrders");

//             // Assert
//             Assert.AreEqual("OkObjectResult", result);
//         }

//         [Test]
//         public void Get_BookById_Returns_Ok_Result()
//         {
//             // Arrange
//             var booksController = GetControllerInstance(_booksControllerName, _bookServiceName);
//             int bookId = 1;

//             // Act
//             var result = GetMethodResult(booksController, "GetBookById", bookId);

//             // Assert
//             Assert.AreEqual("OkObjectResult", result);
//         }

//         [Test]
//         public void Get_OrderById_Returns_Ok_Result()
//         {
//             // Arrange
//             var ordersController = GetControllerInstance(_ordersControllerName, _orderServiceName);
//             int orderId = 1;

//             // Act
//             var result = GetMethodResult(ordersController, "GetOrderById", orderId);

//             // Assert
//             Assert.AreEqual("OkObjectResult", result);
//         }

//         [Test]
//         public void Delete_Book_Returns_NoContent_Result()
//         {
//             // Arrange
//             var booksController = GetControllerInstance(_booksControllerName, _bookServiceName);
//             int bookId = 1;

//             // Act
//             var result = GetMethodResult(booksController, "DeleteBook", bookId);

//             // Assert
//             Assert.AreEqual("NoContentResult", result);
//         }

//         [Test]
//         public void Delete_Order_Returns_NoContent_Result()
//         {
//             // Arrange
//             var ordersController = GetControllerInstance(_ordersControllerName, _orderServiceName);
//             int orderId = 1;

//             // Act
//             var result = GetMethodResult(ordersController, "DeleteOrder", orderId);

//             // Assert
//             Assert.AreEqual("NoContentResult", result);
//         }

//         private string GetControllerInstance(string controllerName, string serviceName)
//         {
//             // Return instance of controller based on controller name and service name
//             switch (controllerName)
//             {
//                 case "BooksController":
//                     return "BooksController(" + GetServiceInstance(serviceName) + ")";
//                 case "OrdersController":
//                     return "OrdersController(" + GetServiceInstance(serviceName) + ")";
//                 default:
//                     return null;
//             }
//         }

//         private string GetServiceInstance(string serviceName)
//         {
//             // Return instance of service based on service name
//             switch (serviceName)
//             {
//                 case "BookService":
//                     return "BookService(" + GetRepositoryInstance(_bookRepositoryName) + ")";
//                 case "OrderService":
//                     return "OrderService(" + GetRepositoryInstance(_orderRepositoryName) + ")";
//                 default:
//                     return null;
//             }
//         }

//         private string GetRepositoryInstance(string repositoryName)
//         {
//             // Return instance of repository based on repository name
//             switch (repositoryName)
//             {
//                 case "BookRepository":
//                     return "BookRepository()";
//                 case "OrderRepository":
//                     return "OrderRepository()";
//                 default:
//                     return null;
//             }
//         }

//         private string GetMethodResult(string controller, string methodName, params object[] args)
//         {
//             // Simulate method calls and return result type as string
//             switch (methodName)
//             {
//                 case "GetAllBooks":
//                 case "GetAllOrders":
//                     return "OkObjectResult";
//                 case "GetBookById":
//                 case "GetOrderById":
//                     return "OkObjectResult";
//                 case "DeleteBook":
//                 case "DeleteOrder":
//                     return "NoContentResult";
//                 default:
//                     return null;
//             }
//         }
//     }
// }
using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;

namespace dotnetapp.Tests
{
    [TestFixture]
    public class BooksControllerTests
    {
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080"); // Base URL of your API
        }

        [Test]
        public async Task GetAllBooks_ReturnsListOfBooks()
        {
            var response = await _httpClient.GetAsync("api/books");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var books = JsonConvert.DeserializeObject<Book[]>(content);

            Assert.IsNotNull(books);
            Assert.IsTrue(books.Length > 0);
        }

        [Test]
        public async Task GetBookById_ValidId_ReturnsBook()
        {
            var bookId = 1;
            var response = await _httpClient.GetAsync($"api/books/{bookId}");

            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var content = await response.Content.ReadAsStringAsync();
            var book = JsonConvert.DeserializeObject<Book>(content);

            Assert.IsNotNull(book);
            Assert.AreEqual(bookId, book.BookId);
        }

        [Test]
        public async Task GetBookById_InvalidId_ReturnsNotFound()
        {
            var bookId = 999;
            var response = await _httpClient.GetAsync($"api/books/{bookId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task AddBook_ReturnsCreatedResponse()
        {
            var newBook = new Book
            {
                BookId = 555, // Explicit data creation
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
        public async Task UpdateBook_ValidId_ReturnsNoContent()
        {
            var bookId = 2;
            var updatedBook = new Book
            {
                BookId = bookId,
                BookName = "Updated Book",
                Category = "Non-fiction",
                Price = 29.99m
            };

            var json = JsonConvert.SerializeObject(updatedBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/books/{bookId}", content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task UpdateBook_InvalidId_ReturnsNotFound()
        {
            var bookId = 999;
            var updatedBook = new Book
            {
                BookId = bookId,
                BookName = "Updated Book",
                Category = "Non-fiction",
                Price = 29.99m
            };

            var json = JsonConvert.SerializeObject(updatedBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/books/{bookId}", content);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteBook_ValidId_ReturnsNoContent()
        {
            var newBook = new Book
            {
                BookId = 777,
                BookName = "Book to Delete",
                Category = "Science",
                Price = 9.99m
            };

            var json = JsonConvert.SerializeObject(newBook);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var createResponse = await _httpClient.PostAsync("api/books", content);
            createResponse.EnsureSuccessStatusCode();

            var createdBook = JsonConvert.DeserializeObject<Book>(await createResponse.Content.ReadAsStringAsync());
            var bookId = createdBook.BookId;

            var response = await _httpClient.DeleteAsync($"api/books/{bookId}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteBook_InvalidId_ReturnsNotFound()
        {
            var bookId = 999;

            var response = await _httpClient.DeleteAsync($"api/books/{bookId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [TearDown]
        public void Cleanup()
        {
            _httpClient.Dispose();
        }
    }
}
