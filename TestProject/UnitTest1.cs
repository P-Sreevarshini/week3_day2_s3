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
using Microsoft.EntityFrameworkCore;



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
        // Test to check that ApplicationDbContext Contains DbSet for model course
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Course()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type ReviewType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Course");
            if (ReviewType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Courses");
            if (propertyInfo == null)
            {
                Assert.Fail("Courses property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(ReviewType), propertyInfo.PropertyType);
            }
        }
        // Test to check that ApplicationDbContext Contains DbSet for model Enquiry
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Enquiry()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type BookingType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Enquiry");
            if (BookingType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Enquiries");
            if (propertyInfo == null)
            {
                Assert.Fail("Enquiries property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(BookingType), propertyInfo.PropertyType);
            }
        }
        // Test to check that ApplicationDbContext Contains DbSet for model Payment
        [Test]
        public void ApplicationDbContext_ContainsDbSet_Payment()
        {
            Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
            Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
            if (contextType == null)
            {
                Assert.Fail("No DbContext found in the assembly");
                return;
            }
            Type ResortType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Payment");
            if (ResortType == null)
            {
                Assert.Fail("No DbSet found in the DbContext");
                return;
            }
            var propertyInfo = contextType.GetProperty("Payments");
            if (propertyInfo == null)
            {
                Assert.Fail("Payments property not found in the DbContext");
                return;
            }
            else
            {
                Assert.AreEqual(typeof(DbSet<>).MakeGenericType(ResortType), propertyInfo.PropertyType);
            }
        }
        [Test] //..............check for customer registration
    public async Task Backend_TestRegisterCustomer()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniquePassword = $"abc@123A";

        string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Customer\"}}";

        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test] //.....................check for customer login

    public async Task Backend_TestLoginUser()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniqueRole = "Customer";

        // Register the user first
        string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"MobileNumber\": \"1234567890\",\"UserRole\": \"{uniqueRole}\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Then try to login
        string requestBody1 = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(requestBody1, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    [Test] //..................check for Admin registration
    public async Task Backend_TestRegisterAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com"; 

        string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test] //................check for Admin login
    public async Task Backend_TestLoginAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";
        string uniqueRole = "Admin";

        // Register the user first
         string requestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        // Then try to login
        string requestBody1 = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(requestBody1, Encoding.UTF8, "application/json"));

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    [Test] //......check for POST resort authorized to Admin
    public async Task Backend_TestPostCourse()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

         string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        
        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        var course = new
        {
            CourseName = "Test course",
            Description = "Test Description",
            Duration = "Test Duration",
            Amount = 1000
        };

        string requestBody = JsonConvert.SerializeObject(course);
        HttpResponseMessage response = await _httpClient.PostAsync("/api/Course", new StringContent(requestBody, Encoding.UTF8, "application/json"));
        
        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
     [Test]
    public async Task Backend_TestPutCourse()
{
    // Register Admin user
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniquePassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string registerRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    // Log in as Admin
    string adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    string responseString = await loginResponse.Content.ReadAsStringAsync();
    dynamic responseMap = JsonConvert.DeserializeObject(responseString);
    string adminAuthToken = responseMap.token;

    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

    // Add a new course
    var Course = new
    {
        CourseName = "Test course",
        Description = "Test Description",
        Duration = "Test Duration",
        Amount = 1000
    };

    string requestBody = JsonConvert.SerializeObject(Course);
    HttpResponseMessage addResortResponse = await _httpClient.PostAsync("/api/Course", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, addResortResponse.StatusCode);

    // Get the added course details
    string addCourseResponseBody = await addResortResponse.Content.ReadAsStringAsync();
    dynamic addResortResponseMap = JsonConvert.DeserializeObject(addResortResponseBody);

    int? resortId = addResortResponseMap?.resortId;

    if (resortId.HasValue)
    {
        var updatedResortDetails = new
        {
            ResortId = resortId, // Corrected variable name and added ResortId
            ResortName = "Updated Resort Name",
            ResortImageUrl = "updated-image-url",
            ResortLocation = "Updated Location",
            ResortAvailableStatus = "Updated Available Status",
            Price = 200,
            Capacity = 30,
            Description = "Updated Description"
        };

        string updateResortRequestBody = JsonConvert.SerializeObject(updatedResortDetails);
        HttpResponseMessage updateResortResponse = await _httpClient.PutAsync($"/api/resort/{resortId}", new StringContent(updateResortRequestBody, Encoding.UTF8, "application/json"));

        // Assert that the resort is updated successfully
        Assert.AreEqual(HttpStatusCode.OK, updateResortResponse.StatusCode);
    }
    else
    {
        // Log additional information for debugging
        string responseContent = await addResortResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Add Resort Response Content: {responseContent}");

        Assert.Fail("Resort ID is null or not found in the response.");
    }
}




    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }

}
