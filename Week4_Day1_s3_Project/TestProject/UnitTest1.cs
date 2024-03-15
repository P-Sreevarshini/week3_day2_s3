using NUnit.Framework;
using System;
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

        [SetUp]
        public void Setup()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = new Uri("http://localhost:8080");
        }

        [Test]
        public async Task GetAllEvents_ReturnsListOfEvents()
        {
            // No need for explicit data creation
            var response = await _httpClient.GetAsync("api/event");
            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var events = JsonConvert.DeserializeObject<Event[]>(content);

            Assert.IsNotNull(events);
            Assert.IsTrue(events.Length > 0);
        }

       [Test]
        public async Task GetEventById_ReturnsEvent()
        {
            // No need for explicit data creation
            var eventId = 3;
            var response = await _httpClient.GetAsync($"api/event/{eventId}");

            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            response.EnsureSuccessStatusCode();

            var content = await response.Content.ReadAsStringAsync();
            var @event = JsonConvert.DeserializeObject<Event>(content);

            Assert.IsNotNull(@event);
            Assert.AreEqual(eventId, @event.EventId);
        }


        [Test]
        public async Task GetEventById_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var eventId = 999;
            var response = await _httpClient.GetAsync($"api/event/{eventId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task CreateEvent_ReturnsCreatedResponse()
        {
            var newEvent = new Event
            {
                EventId = 555, // Explicit data creation
                Name = "New Event",
                Date = DateTime.Now.AddDays(30),
                Location = "New Location"
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/event", content);
            response.EnsureSuccessStatusCode();

            var createdEvent = JsonConvert.DeserializeObject<Event>(await response.Content.ReadAsStringAsync());

            Assert.IsNotNull(createdEvent);
            Assert.AreEqual(newEvent.Name, createdEvent.Name);
        }

        [Test]
        public async Task UpdateEvent_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var eventId = 2;
            var updatedEvent = new Event
            {
                Name = "Updated Event",
                Date = DateTime.Now.AddDays(10),
                Location = "Updated Location"
            };

            var json = JsonConvert.SerializeObject(updatedEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/event/{eventId}", content);

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task UpdateEvent_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var eventId = 999;
            var updatedEvent = new Event
            {
                Name = "Updated Event",
                Date = DateTime.Now.AddDays(10),
                Location = "Updated Location"
            };

            var json = JsonConvert.SerializeObject(updatedEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync($"api/event/{eventId}", content);

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }

        [Test]
        public async Task DeleteEvent_ValidId_ReturnsNoContent()
        {
            // Explicit data creation
            var newEvent = new Event
            {
                EventId = 777,
                Name = "Event to Delete",
                Date = DateTime.Now.AddDays(5),
                Location = "Location to Delete"
            };

            var json = JsonConvert.SerializeObject(newEvent);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var createResponse = await _httpClient.PostAsync("api/event", content);
            createResponse.EnsureSuccessStatusCode();

            var createdEvent = JsonConvert.DeserializeObject<Event>(await createResponse.Content.ReadAsStringAsync());
            var eventId = createdEvent.EventId;

            var response = await _httpClient.DeleteAsync($"api/event/{eventId}");

            Assert.AreEqual(HttpStatusCode.NoContent, response.StatusCode);
        }

        [Test]
        public async Task DeleteEvent_InvalidId_ReturnsNotFound()
        {
            // No need for explicit data creation
            var eventId = 999;

            var response = await _httpClient.DeleteAsync($"api/event/{eventId}");

            Assert.AreEqual(HttpStatusCode.NotFound, response.StatusCode);
        }
    }
}
