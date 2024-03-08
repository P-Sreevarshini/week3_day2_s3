using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Data; 
using System.Reflection;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Net.Http.Headers;




[TestFixture]
public class ApplicationTests
{
    private HttpClient _httpClient;

    [SetUp]
    public void Setup()
    {
        _httpClient = new HttpClient();
        _httpClient.BaseAddress = new Uri("http://localhost:8080");
    }

    [Test]
    public async Task Backend_TestRegisterAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@admin.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

    [Test]
    public async Task Backend_TestLoginAdmin()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@admin.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        // Print registration response
        string registerResponseBody = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Registration Response: " + registerResponseBody);

        // Login with the registered user
        string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
        HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

        // Print login response
        string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
        Console.WriteLine("Login Response: " + loginResponseBody);

        Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    }

    [Test]
    public async Task Backend_TestRegisterUser()
    {
        string uniqueId = Guid.NewGuid().ToString();

        // Generate a unique userName based on a timestamp
        string uniqueUsername = $"abcd_{uniqueId}";
        string uniqueEmail = $"abcd{uniqueId}@gmail.com";

        string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Customer\"}}";
        HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

        Console.WriteLine(response.StatusCode);
        string responseString = await response.Content.ReadAsStringAsync();

        Console.WriteLine(responseString);
        Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
    }

[Test]
public async Task Backend_Test_Post_FixedDepositByAdmin() //add fixed deposit by admin 
{
    string registrationUniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{registrationUniqueId}";
    string uniqueEmail = $"abcd{registrationUniqueId}@admin.com";

    string registrationRequestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registrationRequestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await registrationResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Print login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Login Response: " + loginResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    
    // Extract token from the login response
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string token = loginResponseMap?.Token;

    // Debugging statement to check the retrieved token
    Console.WriteLine("Retrieved Token: " + token);

    Assert.IsNotNull(token);

    // Generate unique data for the fixed deposit
    string depositUniqueId = Guid.NewGuid().ToString();
    decimal amount = 10000; // Sample amount
    int tenureMonths = 12; // Sample tenure in months
    decimal interestRate = 5.5m; // Sample interest rate

    // Construct the request body for the fixed deposit
    string fixedDepositRequestBody = $"{{\"Amount\": {amount}, \"TenureMonths\": {tenureMonths}, \"InterestRate\": {interestRate}}}";

    // Add the token to the request headers
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    // Post the fixed deposit
    HttpResponseMessage fixedDepositResponse = await _httpClient.PostAsync("/api/fixeddeposit", new StringContent(fixedDepositRequestBody, Encoding.UTF8, "application/json"));

    Assert.AreEqual(HttpStatusCode.OK, fixedDepositResponse.StatusCode);
}
[Test]
public async Task Backend_Test_Get_All_FixedDeposits() //get all fixed deposit
{
     string registrationUniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{registrationUniqueId}";
    string uniqueEmail = $"abcd{registrationUniqueId}@admin.com";

    string registrationRequestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registrationRequestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await registrationResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Print login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Login Response: " + loginResponseBody);

    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    
    // Extract token from the login response
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string token = loginResponseMap?.Token;

    // Debugging statement to check the retrieved token
    Console.WriteLine("Retrieved Token: " + token);

    Assert.IsNotNull(token);

    // Generate unique data for the fixed deposit
    string depositUniqueId = Guid.NewGuid().ToString();
    decimal amount = 10000; // Sample amount
    int tenureMonths = 12; // Sample tenure in months
    decimal interestRate = 5.5m; // Sample interest rate

    // Construct the request body for the fixed deposit
    string fixedDepositRequestBody = $"{{\"Amount\": {amount}, \"TenureMonths\": {tenureMonths}, \"InterestRate\": {interestRate}}}";

    // Add the token to the request headers
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    // Post the fixed deposit
    HttpResponseMessage fixedDepositResponse = await _httpClient.PostAsync("/api/fixeddeposit", new StringContent(fixedDepositRequestBody, Encoding.UTF8, "application/json"));

    Assert.AreEqual(HttpStatusCode.OK, fixedDepositResponse.StatusCode);

    // Perform a GET request to retrieve all fixed deposits
    HttpResponseMessage getAllFixedDepositsResponse = await _httpClient.GetAsync("/api/fixeddeposit");

    // Check if the request is successful
    Assert.AreEqual(HttpStatusCode.OK, getAllFixedDepositsResponse.StatusCode);

    // Read the response content
    string getAllFixedDepositsResponseBody = await getAllFixedDepositsResponse.Content.ReadAsStringAsync();

    // Print or process the response body as needed
    Console.WriteLine("All Fixed Deposits Response: " + getAllFixedDepositsResponseBody);
}

[Test]
public async Task Backend_Test_Post_AccountByCustomer() //add account by customer 
{
    string registrationUniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{registrationUniqueId}";
    string uniqueEmail = $"abcd{registrationUniqueId}@gmail.com";

    string registrationRequestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Customer\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registrationRequestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await registrationResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Ensure login is successful
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    // Extract response body from login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

    // Extract user ID from the login response
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    long userId = loginResponseMap.UserId; // Assuming the response contains the user ID
    string token = loginResponseMap?.Token;

    // Debugging statement to check the retrieved token
    Console.WriteLine("Retrieved Token: " + token);

    Assert.IsNotNull(token);

    // Generate unique data for the account
    string accountId = Guid.NewGuid().ToString();
    decimal balance = 10000; // Sample balance
    string accountType = "Savings"; // Sample account type

    // Construct the request body for the account
    string accountRequestBody = $"{{\"UserId\": {userId}, \"Balance\": {balance}, \"AccountType\": \"{accountType}\"}}"; // Include UserId in the request body

    // Add the token to the request headers
    _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    // Post the account
    HttpResponseMessage accountResponse = await _httpClient.PostAsync("/api/account", new StringContent(accountRequestBody, Encoding.UTF8, "application/json"));

    Assert.AreEqual(HttpStatusCode.OK, accountResponse.StatusCode);
}


[Test]
public async Task Backend_Test_Get_All_AccountsByAdmin() //get all accounts by admin 
{
    string registrationUniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{registrationUniqueId}";
    string uniqueEmail = $"abcd{registrationUniqueId}@admin.com";

    string registrationRequestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registrationRequestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await registrationResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Ensure login is successful
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    // Extract response body from login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    // Extract token from the login response
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string token = loginResponseMap?.Token;

    // Debugging statement to check the retrieved token
    Console.WriteLine("Retrieved Token: " + token);

    Assert.IsNotNull(token);

    // Add the token to the request headers
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Make a GET request to retrieve all accounts
    HttpResponseMessage response = await _httpClient.GetAsync("/api/account");

    // Check if the request is successful
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    // Read the response content
    string responseBody = await response.Content.ReadAsStringAsync();

    // Deserialize the response content
    var accounts = JsonConvert.DeserializeObject<IEnumerable<Account>>(responseBody);

    // Ensure that the response contains data
    Assert.IsNotNull(accounts);
    Assert.IsTrue(accounts.Any());
}

[Test]
public async Task Backend_Test_Post_ReviewByCustomer() //post the reviews by customer
{
    string registrationUniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{registrationUniqueId}";
    string uniqueEmail = $"abcd{registrationUniqueId}@gmail.com";

    string registrationRequestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Customer\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registrationRequestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await registrationResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Ensure login is successful
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    // Extract response body from login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

    // Extract user ID from the login response
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    long userId = loginResponseMap.UserId; // Assuming the response contains the user ID
    string token = loginResponseMap?.Token;

    // Debugging statement to check the retrieved token
    Console.WriteLine("Retrieved Token: " + token);
    Console.WriteLine("Retrieved User ID: " + userId);

    Assert.IsNotNull(token);

    // Generate unique data for the review
    string body = "This is a sample review body";
    int rating = 5;
    DateTime dateCreated = DateTime.UtcNow;

    // Construct the request body for the review
    string reviewRequestBody = $"{{\"UserId\": {userId}, \"Body\": \"{body}\", \"Rating\": {rating}, \"DateCreated\": \"{dateCreated:yyyy-MM-ddTHH:mm:ss.fffZ}\"}}";

    // Add the token to the request headers
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Post the review
    HttpResponseMessage reviewResponse = await _httpClient.PostAsync("/api/review", new StringContent(reviewRequestBody, Encoding.UTF8, "application/json"));

    Assert.AreEqual(HttpStatusCode.OK, reviewResponse.StatusCode);
}

[Test]
public async Task Backend_Test_Get_All_ReviewsByAdmin() //get all the reviews by admin
{
    string registrationUniqueId = Guid.NewGuid().ToString();

    // Generate a unique userName based on a timestamp
    string uniqueUsername = $"abcd_{registrationUniqueId}";
    string uniqueEmail = $"abcd{registrationUniqueId}@admin.com";

    string registrationRequestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registrationRequestBody, Encoding.UTF8, "application/json"));

    // Print registration response
    string registerResponseBody = await registrationResponse.Content.ReadAsStringAsync();
    Console.WriteLine("Registration Response: " + registerResponseBody);

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    // Ensure login is successful
    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

    // Extract response body from login response
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();

    // Extract user ID from the login response
    dynamic loginResponseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string token = loginResponseMap?.Token;

    // Debugging statement to check the retrieved token
    Console.WriteLine("Retrieved Token: " + token);

    Assert.IsNotNull(token);

    // Add the token to the request headers
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Make a GET request to retrieve all reviews
    HttpResponseMessage response = await _httpClient.GetAsync("/api/Review");

    // Check if the request is successful
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    // Read the response content
    string responseBody = await response.Content.ReadAsStringAsync();

    // Deserialize the response content
    var reviews = JsonConvert.DeserializeObject<IEnumerable<Review>>(responseBody);

    // Ensure that the response contains data
    Assert.IsNotNull(reviews);
    Assert.IsTrue(reviews.Any());
}

 [Test]
        public async Task Backend_Test_Post_FD_Account()
        {
            // Generate unique user data for registration
            string registrationUniqueId = Guid.NewGuid().ToString();
            string uniqueUsername = $"user_{registrationUniqueId}";
            string uniqueEmail = $"user{registrationUniqueId}@example.com";
            string userPassword = "password123";

            // Registration request body
            string registrationRequestBody = JsonConvert.SerializeObject(new
            {
                Username = uniqueUsername,
                Password = userPassword,
                Email = uniqueEmail,
                UserRole = "Customer"
            });

            // Register the user
            HttpResponseMessage registrationResponse = await _httpClient.PostAsync("/api/register", new StringContent(registrationRequestBody, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, registrationResponse.StatusCode);

            // Login with the registered user
            string loginRequestBody = JsonConvert.SerializeObject(new
            {
                Email = uniqueEmail,
                Password = userPassword
            });
            HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));
            Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);

            // Extract user ID and token from the login response
            dynamic loginResponseData = JsonConvert.DeserializeObject(await loginResponse.Content.ReadAsStringAsync());
            long userId = loginResponseData.UserId;
            string token = loginResponseData.Token;

            Assert.IsFalse(string.IsNullOrEmpty(token));
            Assert.IsTrue(userId > 0);

            // Construct FD Account data
            var fdAccount = new
            {
                UserId = userId,
                Status = "Pending", // Assuming the status is pending for a new FD account
                // You can add more properties here as needed
            };

            // Post the FD account
            string fdAccountRequestBody = JsonConvert.SerializeObject(fdAccount);
            _httpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
            HttpResponseMessage postFdAccountResponse = await _httpClient.PostAsync("/api/FDAccount", new StringContent(fdAccountRequestBody, Encoding.UTF8, "application/json"));

            Assert.AreEqual(HttpStatusCode.Created, postFdAccountResponse.StatusCode);
        }
    }










[Test]
public void Backend_Test_ApplicationDbContext_ContainsDbSet_User()
{
    Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
    Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
    if (contextType == null)
    {
        Assert.Fail("No DbContext found in the assembly");
        return;
    }
    Type userType = assembly.GetTypes().FirstOrDefault(t => t.Name == "User");
    if (userType == null)
    {
        Assert.Fail("No User entity found in the assembly");
        return;
    }
    var propertyInfo = contextType.GetProperty("Users", typeof(DbSet<>).MakeGenericType(userType));
    if (propertyInfo == null)
    {
        Assert.Fail("Users property not found in the DbContext");
        return;
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(userType), propertyInfo.PropertyType);
    }
}
[Test]
public void Backend_Test_ApplicationDbContext_ContainsDbSet_Account()
{
    Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
    Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
    if (contextType == null)
    {
        Assert.Fail("No DbContext found in the assembly");
        return;
    }
    Type accountType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Account");
    if (accountType == null)
    {
        Assert.Fail("No Account entity found in the assembly");
        return;
    }
    var propertyInfo = contextType.GetProperty("Accounts", typeof(DbSet<>).MakeGenericType(accountType));
    if (propertyInfo == null)
    {
        Assert.Fail("Accounts property not found in the DbContext");
        return;
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(accountType), propertyInfo.PropertyType);
    }
}
[Test]
public void Backend_Test_ApplicationDbContext_ContainsDbSet_FixedDeposit()
{
    Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
    Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
    if (contextType == null)
    {
        Assert.Fail("No DbContext found in the assembly");
        return;
    }
    Type accountType = assembly.GetTypes().FirstOrDefault(t => t.Name == "FixedDeposit");
    if (accountType == null)
    {
        Assert.Fail("No FixedDeposit entity found in the assembly");
        return;
    }
    var propertyInfo = contextType.GetProperty("FixedDeposits", typeof(DbSet<>).MakeGenericType(accountType));
    if (propertyInfo == null)
    {
        Assert.Fail("FixedDeposits property not found in the DbContext");
        return;
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(accountType), propertyInfo.PropertyType);
    }
}

[Test]
public void Backend_Test_ApplicationDbContext_ContainsDbSet_Review()
{
    Assembly assembly = Assembly.GetAssembly(typeof(ApplicationDbContext));
    Type contextType = assembly.GetTypes().FirstOrDefault(t => typeof(DbContext).IsAssignableFrom(t));
    if (contextType == null)
    {
        Assert.Fail("No DbContext found in the assembly");
        return;
    }
    Type accountType = assembly.GetTypes().FirstOrDefault(t => t.Name == "Review");
    if (accountType == null)
    {
        Assert.Fail("No Review entity found in the assembly");
        return;
    }
    var propertyInfo = contextType.GetProperty("Reviews", typeof(DbSet<>).MakeGenericType(accountType));
    if (propertyInfo == null)
    {
        Assert.Fail("Reviews property not found in the DbContext");
        return;
    }
    else
    {
        Assert.AreEqual(typeof(DbSet<>).MakeGenericType(accountType), propertyInfo.PropertyType);
    }
}

[TearDown]
    public void TearDown()
    {
        _httpClient.Dispose();
    }

}