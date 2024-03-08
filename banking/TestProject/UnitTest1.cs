using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers; 
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Newtonsoft.Json;
using dotnetapp.Models; // Add this using directive at the top of the file



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
public async Task Backend_Test_Post_FixedDepositByAdmin()
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
public async Task Backend_Test_Get_All_FixedDeposits()
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

    // Perform login to get the token
    string uniqueId = Guid.NewGuid().ToString();
    string uniqueEmail = $"abcd{uniqueId}@admin.com";

    // Login with the registered user
    string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; 
    HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    
    string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    dynamic responseMap = JsonConvert.DeserializeObject(loginResponseBody);
    string token = responseMap.token;
    Assert.IsNotNull(token);

    // Add the token to the request headers
    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

    // Make a GET request to retrieve all fixed deposits
    HttpResponseMessage response = await _httpClient.GetAsync("/api/fixeddeposit");

    // Check if the request is successful
    Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

    // Read the response content
    string responseBody = await response.Content.ReadAsStringAsync();

    // Deserialize the response content
    var fixedDeposits = JsonConvert.DeserializeObject<IEnumerable<FixedDeposit>>(responseBody);

    // Ensure that the response contains data
    Assert.IsNotNull(fixedDeposits);
    Assert.IsTrue(fixedDeposits.Any());
}



    // [Test, Order(6)]
    // public async Task Backend_Test_Post_ProductByInventoryManager_Forbidden()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"InventoryManager\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //     string token = responseMap.token;

    //     Assert.IsNotNull(token);

    //     string uniquetitle = Guid.NewGuid().ToString();

    //     // Use a dynamic and unique userName for admin (appending timestamp)
    //     string uniqueprodTitle = $"prodTitle_{uniquetitle}";

    //     string giftJson = $"{{\"Name\":\"{uniqueprodTitle}\",\"Description\":\"test\",\"Price\":250,\"Quantity\":10}}";
    //     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //     HttpResponseMessage prodresponse = await _httpClient.PostAsync("/api/product",
    //         new StringContent(giftJson, Encoding.UTF8, "application/json"));

    //     Assert.AreEqual(HttpStatusCode.Forbidden, prodresponse.StatusCode);
    // }

    // [Test, Order(7)]
    // public async Task Backend_TestGetAllproductsByBoth()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //     string token = responseMap.token;

    //     Assert.IsNotNull(token);


    //     Console.WriteLine("admin111" + token);
    //     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    //     HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/product");

    //     Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    // }

    // [Test, Order(8)]
    // public async Task Backend_Test_Post_CustomerByInventoryManager_Forbidden()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"InventoryManager\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //     string token = responseMap.token;

    //     Assert.IsNotNull(token);

    //     string uniquetitle = Guid.NewGuid().ToString();

    //     // Use a dynamic and unique userName for admin (appending timestamp)
    //     string uniqueCustName = $"prodTitle_{uniquetitle}";

    //     string giftJson = $"{{\"CustomerName\":\"{uniqueCustName}\",\"MobileNumber\":\"7894561232\",\"Email\":\"{uniqueCustName}@gmail.com\"}}";
    //     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //     HttpResponseMessage prodresponse = await _httpClient.PostAsync("/api/customer",
    //         new StringContent(giftJson, Encoding.UTF8, "application/json"));

    //     Assert.AreEqual(HttpStatusCode.Forbidden, prodresponse.StatusCode);
    // }

    // [Test, Order(9)]
    // public async Task Backend_Test_Post_CustomerByAdmin_Ok()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //     string token = responseMap.token;

    //     Assert.IsNotNull(token);

    //     string uniquetitle = Guid.NewGuid().ToString();

    //     // Use a dynamic and unique userName for admin (appending timestamp)
    //     string uniqueCustName = $"prodTitle_{uniquetitle}";

    //     string giftJson = $"{{\"CustomerName\":\"{uniqueCustName}\",\"MobileNumber\":\"7894561232\",\"Email\":\"{uniqueCustName}@gmail.com\"}}";
    //     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
    //     HttpResponseMessage prodresponse = await _httpClient.PostAsync("/api/customer",
    //         new StringContent(giftJson, Encoding.UTF8, "application/json"));

    //     Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    // }

    // [Test, Order(10)]
    // public async Task Backend_TestGetAllCustomerByBoth()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //     string token = responseMap.token;

    //     Assert.IsNotNull(token);


    //     Console.WriteLine("admin111" + token);
    //     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    //     HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/customer");

    //     Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    // }

    // [Test, Order(11)]
    // public async Task Backend_TestGetAllCustomerByBoth_WithoutToken_Should_Unauthorized()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);


    //     HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/customer");

    //     Assert.AreEqual(HttpStatusCode.Unauthorized, prodresponse.StatusCode);
    // }

    // [Test, Order(12)]
    // public async Task Backend_TestGetAllNotificationByAdmin()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"Admin\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //     string token = responseMap.token;

    //     Assert.IsNotNull(token);


    //     Console.WriteLine("admin111" + token);
    //     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    //     HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/notification");

    //     Assert.AreEqual(HttpStatusCode.OK, prodresponse.StatusCode);
    // }


    // [Test, Order(13)]
    // public async Task Backend_TestGetAllNotificationByInventoryManager_Forbidden()
    // {
    //     string uniqueId = Guid.NewGuid().ToString();

    //     // Generate a unique userName based on a timestamp
    //     string uniqueUsername = $"abcd_{uniqueId}";
    //     string uniqueEmail = $"abcd{uniqueId}@gmail.com";

    //     string requestBody = $"{{\"Username\": \"{uniqueUsername}\", \"Password\": \"abc@123A\", \"Email\": \"{uniqueEmail}\", \"MobileNumber\": \"1234567890\", \"UserRole\": \"InventoryManager\"}}";
    //     HttpResponseMessage response = await _httpClient.PostAsync("/api/register", new StringContent(requestBody, Encoding.UTF8, "application/json"));

    //     // Print registration response
    //     string registerResponseBody = await response.Content.ReadAsStringAsync();
    //     Console.WriteLine("Registration Response: " + registerResponseBody);

    //     // Login with the registered user
    //     string loginRequestBody = $"{{\"Email\" : \"{uniqueEmail}\",\"Password\" : \"abc@123A\"}}"; // Updated variable names
    //     HttpResponseMessage loginResponse = await _httpClient.PostAsync("/api/login", new StringContent(loginRequestBody, Encoding.UTF8, "application/json"));

    //     // Print login response
    //     string loginResponseBody = await loginResponse.Content.ReadAsStringAsync();
    //     Console.WriteLine("Login Response: " + loginResponseBody);

    //     Assert.AreEqual(HttpStatusCode.OK, loginResponse.StatusCode);
    //     string responseBody = await loginResponse.Content.ReadAsStringAsync();

    //     dynamic responseMap = JsonConvert.DeserializeObject(responseBody);

    //     string token = responseMap.token;

    //     Assert.IsNotNull(token);


    //     Console.WriteLine("admin111" + token);
    //     _httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);

    //     HttpResponseMessage prodresponse = await _httpClient.GetAsync("/api/notification");

    //     Assert.AreEqual(HttpStatusCode.Forbidden, prodresponse.StatusCode);
    // }


}