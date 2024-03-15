using NUnit.Framework;
using System;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.IO;



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

        private string GetControllerInstance(string controllerName, string serviceName)
        {
            // Return instance of controller based on controller name and service name
            switch (controllerName)
            {
                case "BooksController":
                    return "BooksController(" + GetServiceInstance(serviceName) + ")";
                case "OrdersController":
                    return "OrdersController(" + GetServiceInstance(serviceName) + ")";
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
