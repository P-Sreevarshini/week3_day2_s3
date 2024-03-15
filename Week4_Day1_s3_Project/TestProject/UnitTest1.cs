using NUnit.Framework;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using dotnetapp.Models;

namespace EventManagementAPITests
{
    [TestFixture]
    public class dotnetappApplicationTests
    {
        private HttpClient _httpClient;
        private string _generatedToken;

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("https://8080-dfbbeddfccdbcfacbdcbaeadbebabcdebdca.premiumproject.examly.io/"); 
        }

        [Test]
        public async Task GetAllEvents_ReturnsListOfEvents()
        {
            var response = await _httpClient.GetAsync("api/event"); // Corrected route
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<Event[]>(content);

            Assert.IsNotNull(events);
            Assert.IsTrue(events.Length > 0);
        }

       [Test]
        public async Task GetEventById_ReturnsEvent()
        {
            var eventId = 1; // Update with an existing event ID
            var response = await _httpClient.GetAsync($"api/event/{eventId}"); // Corrected route
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var @event = JsonConvert.DeserializeObject<Event>(content);

            Assert.IsNotNull(@event);
            Assert.AreEqual(eventId, @event.EventId);
        }


        [Test]
        public async Task GetEventById_InvalidId_ReturnsNotFound()
        {
            var eventId = 999; // Update with a non-existing event ID
            var response = await _httpClient.GetAsync($"api/event/{eventId}"); // Corrected route

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task CreateEvent_ReturnsCreatedResponse()
        {
            var newEvent = new Event
            {
                Name = "New Event",
                Date = DateTime.Now.AddDays(30),
                Location = "New Location"
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/event", content); // Corrected route
            response.EnsureSuccessStatusCode();

            var createdEvent = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdEvent);
            Assert.AreEqual(newEvent.Name, createdEvent.Name);
            // Add more assertions as needed
        }

        [Test]
        public async Task UpdateEvent_ValidId_ReturnsNoContent()
        {
            var eventId = 1; // Update with an existing event ID
            var updatedEvent = new Event
            {
                Name = "Updated Event",
                Date = DateTime.Now.AddDays(10),
                Location = "Updated Location"
            };

            var json = JsonConvert.SerializeObject(updatedEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/event/{eventId}", content); // Corrected route

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task UpdateEvent_InvalidId_ReturnsNotFound()
        {
            var eventId = 999; // Update with a non-existing event ID
            var updatedEvent = new Event
            {
                Name = "Updated Event",
                Date = DateTime.Now.AddDays(10),
                Location = "Updated Location"
            };

            var json = JsonConvert.SerializeObject(updatedEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/event/{eventId}", content); // Corrected route

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteEvent_ValidId_ReturnsNoContent()
        {
            var eventId = 1; // Update with an existing event ID

            var response = await _httpClient.GetAsync($"api/event/{eventId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteEvent_InvalidId_ReturnsNotFound()
        {
            var eventId = 999; // Update with a non-existing event ID

            var response = await _httpClient.DeleteAsync($"api/event/{eventId}"); // Corrected route

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

    }
}