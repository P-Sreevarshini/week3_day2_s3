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
    HttpResponseMessage addCourseResponse = await _httpClient.PostAsync("/api/Course", new StringContent(requestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.Created, addCourseResponse.StatusCode);

    // Get the added course details
    string addCourseResponseBody = await addCourseResponse.Content.ReadAsStringAsync();
    dynamic addCourseResponseMap = JsonConvert.DeserializeObject(addCourseResponseBody);

    int? courseId = addCourseResponseMap?.courseID; // Adjust the property name here

    if (courseId.HasValue)
    {
        var updatedCourseDetails = new
        {
            CourseID = courseId,  // Adjust the property name here
            CourseName = "Updated Test course", // Update the course details
            Description = "Updated Test Description",
            Duration = "Updated Test Duration",
            Amount = 2000
        };

        string updateCourseRequestBody = JsonConvert.SerializeObject(updatedCourseDetails);
        HttpResponseMessage updateCourseResponse = await _httpClient.PutAsync($"/api/Course/{courseId}", new StringContent(updateCourseRequestBody, Encoding.UTF8, "application/json"));

        // Assert that the course is updated successfully
        Assert.AreEqual(HttpStatusCode.OK, updateCourseResponse.StatusCode);
    }
    else
    {
        // Log additional information for debugging
        string responseContent = await addCourseResponse.Content.ReadAsStringAsync();
        Console.WriteLine($"Add Course Response Content: {responseContent}");

        Assert.Fail("Course ID is null or not found in the response.");
    }
}
 [Test] //..........Check for GET course 
    public async Task Backend_TestGetAllCourse()
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

        HttpResponseMessage getBookingsResponse = await _httpClient.GetAsync("/api/Course");
        
        Assert.AreEqual(HttpStatusCode.OK, getBookingsResponse.StatusCode);
    }
[Test] // Check GET course by CourseID
public async Task Backend_TestGetCourseByCourseID()
{
    // Register a customer user
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueUsername = $"abcd_{uniqueId}";
    string uniquePassword = $"abcdA{uniqueId}@123";
    string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    string registerRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registerRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

    // Log in as Customer
    string customerLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(customerLoginRequestBody, Encoding.UTF8, "application/json"));
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    string responseString = await loginResponse.Content.ReadAsStringAsync();
    dynamic responseMap = JsonConvert.DeserializeObject(responseString);
    string customerAuthToken = responseMap.token;

    _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", customerAuthToken);

    // Make a GET request to get a course by CourseID
    HttpResponseMessage getCourseByCourseIdResponse = await _httpClient.GetAsync($"/api/Course/{responseMap.courseId}");
    Assert.AreEqual(HttpStatusCode.OK, getCourseByCourseIdResponse.StatusCode);

    // Deserialize the response content as a list of Course objects
    string responseBody = await getCourseByCourseIdResponse.Content.ReadAsStringAsync();
    var courses = JsonConvert.DeserializeObject<List<Course>>(responseBody);

    // Assert that the returned list of courses is not null and contains at least one course
    Assert.IsNotNull(courses);
    Assert.IsTrue(courses.Count > 0);
}
[Test] // Check for DELETE course
public async Task Backend_TestDeleteCourse()
{
        int courseIdToDelete = 1; // Provide the course ID to delete
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

    // Act: Delete the course
    var deleteCourseResponse = await _httpClient.DeleteAsync($"/api/Course/{courseIdToDelete}");
    // Assert
    Assert.AreEqual(HttpStatusCode.NotFound, deleteCourseResponse.StatusCode);

    // Verify that the course is deleted
    var verifyDeleteResponse = await _httpClient.GetAsync($"/api/Course/{courseIdToDelete}");
    Assert.AreEqual(HttpStatusCode.NotFound, verifyDeleteResponse.StatusCode);
}

 [Test] //......check for Enquiry resort authorized to Customer
    public async Task Backend_TestPostEnquiry()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

         string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Customer\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        
        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        var enquiry = new
        {
            EnquiryDate = DateTime.Now,
            Title = "Test Enquiry",
            Description = "Test Description",
            EmailID = "test@example.com",
            EnquiryType = "General",
            CourseID = 3, // Assuming the ID of the related course
            UserId = 1 // Assuming the ID of the related user
        };

    Console.WriteLine(enquiry);
        string requestBody = JsonConvert.SerializeObject(enquiry);
        HttpResponseMessage response = await _httpClient.PostAsync("/api/Enquiry", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            Console.WriteLine(enquiry);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }

[Test] // Check for DELETE Enquiry
public async Task Backend_TestDeleteEnquiry()
{
            int courseIdToDelete = 1; // Provide the course ID to delete

       string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

         string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Customer\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        
        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        var enquiry = new
        {
            EnquiryDate = DateTime.Now,
            Title = "Test Enquiry",
            Description = "Test Description",
            EmailID = "test@example.com",
            EnquiryType = "General",
            CourseID = 3, // Assuming the ID of the related course
            UserId = 1 // Assuming the ID of the related user
        };

    Console.WriteLine(enquiry);
        string requestBody = JsonConvert.SerializeObject(enquiry);
        HttpResponseMessage response = await _httpClient.PostAsync("/api/Enquiry", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            Console.WriteLine(enquiry);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);

    // Act: Delete the course
    var deleteCourseResponse = await _httpClient.DeleteAsync($"/api/Enquiry/{courseIdToDelete}");
    // Assert
    Assert.AreEqual(HttpStatusCode.NoContent, deleteCourseResponse.StatusCode);

    // Verify that the course is deleted
    var verifyDeleteResponse = await _httpClient.GetAsync($"/api/Enquiry/{courseIdToDelete}");
    Assert.AreEqual(HttpStatusCode.NotFound, verifyDeleteResponse.StatusCode);
}

[Test] //..........Check for GET course 
    public async Task Backend_TestGetAllEnquries()
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

        HttpResponseMessage getBookingsResponse = await _httpClient.GetAsync("/api/Enquiry");
        
        Assert.AreEqual(HttpStatusCode.OK, getBookingsResponse.StatusCode);
    }
    [Test] //..........Check for GET Payment 
    public async Task Backend_TestGetAllPayment()
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

        HttpResponseMessage getBookingsResponse = await _httpClient.GetAsync("/api/Payment");
        
        Assert.AreEqual(HttpStatusCode.OK, getBookingsResponse.StatusCode);
    }
[Test] //......check for Enquiry resort authorized to Customer
    public async Task Backend_TestPostPayment()
    {
        string uniqueId = Guid.NewGuid().ToString();
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniquePassword = $"abcdA{uniqueId}@123";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

         string RegisterrequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\", \"Username\": \"{uniqueUsername}\", \"UserRole\": \"Customer\"}}";
        HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(RegisterrequestBody, Encoding.UTF8, "application/json"));
         Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

        
        var adminLoginRequestBody = $"{{\"Email\": \"{uniqueEmail}\", \"Password\": \"{uniquePassword}\"}}";
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(adminLoginRequestBody, Encoding.UTF8, "application/json"));
        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

        string responseString = await loginResponse.Content.ReadAsStringAsync();
        dynamic responseMap = JsonConvert.DeserializeObject(responseString);
        string adminAuthToken = responseMap.token;

        _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", adminAuthToken);

        var enquiry = new
        {
            EnquiryDate = DateTime.Now,
            Title = "Test Enquiry",
            Description = "Test Description",
            EmailID = "test@example.com",
            EnquiryType = "General",
            CourseID = 3, // Assuming the ID of the related course
            UserId = 1 // Assuming the ID of the related user
        };

    Console.WriteLine(enquiry);
        string requestBody = JsonConvert.SerializeObject(enquiry);
        HttpResponseMessage response = await _httpClient.PostAsync("/api/Enquiry", new StringContent(requestBody, Encoding.UTF8, "application/json"));
            Console.WriteLine(enquiry);

        Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
    }
    [TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }

}
