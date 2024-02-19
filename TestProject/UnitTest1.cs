using NUnit.Framework;
using System;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;




[TestFixture]
public class dotnetappApplicationTests
{
    private HttpClient _httpClient;
    private string _generatedToken;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080"); 
    }


// Test to check that ApplicationDbContext Contains DbSet for model User
        [Test]
        public void ApplicationDbContext_ContainsDbSet_User()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type UserType = assembly.GetTypes().FirstOrDefault(t => t.Name == "User");
            if (UserType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Users");
            if (propertyInfo == null)
            {
                Assert.Fail("Users property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(UserType), propertyInfo.PropertyType);
            }
        }
        // Test to check that ApplicationDbContext Contains DbSet for model Review
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Review()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type ReviewType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Review");
            if (ReviewType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Reviews");
            if (propertyInfo == null)
            {
                Assert.Fail("Reviews property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(ReviewType), propertyInfo.PropertyType);
            }
        }
        // Test to check that ApplicationDbContext Contains DbSet for model Booking
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Booking()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type BookingType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Booking");
            if (BookingType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Bookings");
            if (propertyInfo == null)
            {
                Assert.Fail("Bookings property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(BookingType), propertyInfo.PropertyType);
            }
        }
        // Test to check that ApplicationDbContext Contains DbSet for model Resort
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Resort()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type ResortType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Resort");
            if (ResortType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Resorts");
            if (propertyInfo == null)
            {
                Assert.Fail("Resorts property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(ResortType), propertyInfo.PropertyType);
            }
        }

    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }

}
