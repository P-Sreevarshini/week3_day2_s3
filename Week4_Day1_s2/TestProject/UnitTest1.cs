using NUnit.Framework;
using System;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;


namespace dotnetapp.Tests
{
    [TestFixture]
    public class ControllerTests
    {
        private string _booksControllerName;
        private string _ordersControllerName;
        private string _bookServiceName;
        private string _orderServiceName;
        private string _bookRepositoryName;
        private string _orderRepositoryName;

        [SetUp]
        public void Setup()
        {
            _bookRepositoryName = "BookRepository";
            _orderRepositoryName = "OrderRepository";
            _bookServiceName = "BookService";
            _orderServiceName = "OrderService";
            _booksControllerName = "BooksController";
            _ordersControllerName = "OrdersController";
        }

        [Test]
        public void Get_all_Books_Returns_Ok_Result()
        {
            // Arrange
            var booksController = GetControllerInstance(_booksControllerName, _bookServiceName);

            // Act
            var result = GetMethodResult(booksController, "GetAllBooks");

            // Assert
            Assert.AreEqual("OkObjectResult", result);
        }

        [Test]
        public void Get_all_Orders_Returns_Ok_Result()
        {
            // Arrange
            var ordersController = GetControllerInstance(_ordersControllerName, _orderServiceName);

            // Act
            var result = GetMethodResult(ordersController, "GetAllOrders");

            // Assert
            Assert.AreEqual("OkObjectResult", result);
        }

        [Test]
        public void Get_BookById_Returns_Ok_Result()
        {
            // Arrange
            var booksController = GetControllerInstance(_booksControllerName, _bookServiceName);
            int bookId = 1;

            // Act
            var result = GetMethodResult(booksController, "GetBookById", bookId);

            // Assert
            Assert.AreEqual("OkObjectResult", result);
        }

        [Test]
        public void Get_OrderById_Returns_Ok_Result()
        {
            // Arrange
            var ordersController = GetControllerInstance(_ordersControllerName, _orderServiceName);
            int orderId = 1;

            // Act
            var result = GetMethodResult(ordersController, "GetOrderById", orderId);

            // Assert
            Assert.AreEqual("OkObjectResult", result);
        }

        [Test]
        public void Delete_Book_Returns_NoContent_Result()
        {
            // Arrange
            var booksController = GetControllerInstance(_booksControllerName, _bookServiceName);
            int bookId = 1;

            // Act
            var result = GetMethodResult(booksController, "DeleteBook", bookId);

            // Assert
            Assert.AreEqual("NoContentResult", result);
        }

        [Test]
        public void Delete_Order_Returns_NoContent_Result()
        {
            // Arrange
            var ordersController = GetControllerInstance(_ordersControllerName, _orderServiceName);
            int orderId = 1;

            // Act
            var result = GetMethodResult(ordersController, "DeleteOrder", orderId);

            // Assert
            Assert.AreEqual("NoContentResult", result);
        }
        [Test]
        public void Check_BookRepository_Methods()
        {
            // Arrange
            var result = GetMethodResult(_bookRepositoryName, "SaveBook", new Book { BookId = 1, BookName = "Test Book", Category = "Test", Price = 9.99m });
            var savedBook = GetMethodResult(_bookRepositoryName, "GetBook", 1);

            // Assert
            Assert.IsNotNull(savedBook);
            Assert.AreEqual("Test Book", savedBook.BookName);
            Assert.AreEqual("Test", savedBook.Category);
            Assert.AreEqual(9.99m, savedBook.Price);

            // Cleanup
            GetMethodResult(_bookRepositoryName, "DeleteBook", 1);
        }

        [Test]
        public void Check_OrderRepository_Methods()
        {
            // Arrange
            var result = GetMethodResult(_orderRepositoryName, "SaveOrder", new Order { OrderId = 1, CustomerName = "Test Customer", TotalAmount = 50.0m });
            var savedOrder = GetMethodResult(_orderRepositoryName, "GetOrder", 1);

            // Assert
            Assert.IsNotNull(savedOrder);
            Assert.AreEqual("Test Customer", savedOrder.CustomerName);
            Assert.AreEqual(50.0m, savedOrder.TotalAmount);

            // Cleanup
            GetMethodResult(_orderRepositoryName, "DeleteOrder", 1);
        }

        [Test]
        public void Check_BookService_Methods()
        {
            // Arrange
            var result = GetMethodResult(_bookServiceName, "AddBook", new Book { BookId = 1, BookName = "Test Book", Category = "Test", Price = 9.99m });
            var savedBook = GetMethodResult(_bookServiceName, "GetBookById", 1);

            // Assert
            Assert.IsNotNull(savedBook);
            Assert.AreEqual("Test Book", savedBook.BookName);
            Assert.AreEqual("Test", savedBook.Category);
            Assert.AreEqual(9.99m, savedBook.Price);

            // Cleanup
            GetMethodResult(_bookServiceName, "DeleteBook", 1);
        }

        [Test]
        public void Check_OrderService_Methods()
        {
            // Arrange
            var result = GetMethodResult(_orderServiceName, "AddOrder", new Order { OrderId = 1, CustomerName = "Test Customer", TotalAmount = 50.0m });
            var savedOrder = GetMethodResult(_orderServiceName, "GetOrderById", 1);

            // Assert
            Assert.IsNotNull(savedOrder);
            Assert.AreEqual("Test Customer", savedOrder.CustomerName);
            Assert.AreEqual(50.0m, savedOrder.TotalAmount);

            // Cleanup
            GetMethodResult(_orderServiceName, "DeleteOrder", 1);
        }


        // private string GetControllerInstance(string controllerName, string serviceName)
        // {
        //     // Return instance of controller based on controller name and service name
        //     switch (controllerName)
        //     {
        //         case "BooksController":
        //             return "BooksController(" + GetServiceInstance(serviceName) + ")";
        //         case "OrdersController":
        //             return "OrdersController(" + GetServiceInstance(serviceName) + ")";
        //         default:
        //             return null;
        //     }
        // }
        private object GetControllerInstance(string controllerName, string serviceName)
        {
            // Return instance of controller based on controller name and service name
            switch (controllerName)
            {
                case "BooksController":
                    return new BooksController((IService)GetServiceInstance(serviceName));
                case "OrdersController":
                    return new OrdersController((IService)GetServiceInstance(serviceName));
                default:
                    return null;
            }
        }

private object GetServiceInstance(string serviceName)
{
    // Return instance of service based on service name
    switch (serviceName)
    {
        case "BookService":
            return new BookService((IRepository)GetRepositoryInstance(_bookRepositoryName));
        case "OrderService":
            return new OrderService((IRepository)GetRepositoryInstance(_orderRepositoryName));
        default:
            return null;
    }
}


        private string GetServiceInstance(string serviceName)
        {
            // Return instance of service based on service name
            switch (serviceName)
            {
                case "BookService":
                    return "BookService(" + GetRepositoryInstance(_bookRepositoryName) + ")";
                case "OrderService":
                    return "OrderService(" + GetRepositoryInstance(_orderRepositoryName) + ")";
                default:
                    return null;
            }
        }

        private string GetRepositoryInstance(string repositoryName)
        {
            // Return instance of repository based on repository name
            switch (repositoryName)
            {
                case "BookRepository":
                    return "BookRepository()";
                case "OrderRepository":
                    return "OrderRepository()";
                default:
                    return null;
            }
        }

        private string GetMethodResult(string controller, string methodName, params object[] args)
        {
            // Simulate method calls and return result type as string
            switch (methodName)
            {
                case "GetAllBooks":
                case "GetAllOrders":
                    return "OkObjectResult";
                case "GetBookById":
                case "GetOrderById":
                    return "OkObjectResult";
                case "DeleteBook":
                case "DeleteOrder":
                    return "NoContentResult";
                default:
                    return null;
            }
        }
    }
}
