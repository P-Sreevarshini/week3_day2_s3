// Services/EventService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using dotnetapp.Models; // Update the namespace to dotnetapp.Models

namespace dotnetapp.Services // Update the namespace to dotnetapp.Services
{
    public class EventService
    {
        private readonly List<Event> _events;

        public EventService()
        {
            _events = new List<Event>
            {
                new Event { EventId = 1, Name = "Event 1", Date = DateTime.Now.AddDays(7), Location = "Location 1" },
                new Event { EventId = 2, Name = "Event 2", Date = DateTime.Now.AddDays(14), Location = "Location 2" },
                new Event { EventId = 3, Name = "Event 3", Date = DateTime.Now.AddDays(21), Location = "Location 3" }
            };
        }

        public IEnumerable<Event> GetAllEvents()
        {
            return _events;
        }

        public Event GetEventById(int eventId)
        {
            return _events.FirstOrDefault(e => e.EventId == eventId);
        }

        public void AddEvent(Event newEvent)
        {
            newEvent.EventId = _events.Count + 1;
            _events.Add(newEvent);
        }

        public void UpdateEvent(int eventId, Event updatedEvent)
        {
            var existingEvent = _events.FirstOrDefault(e => e.EventId == eventId);
            if (existingEvent != null)
            {
                existingEvent.Name = updatedEvent.Name;
                existingEvent.Date = updatedEvent.Date;
                existingEvent.Location = updatedEvent.Location;
            }
        }

        public void DeleteEvent(int eventId)
        {
            var existingEvent = _events.FirstOrDefault(e => e.EventId == eventId);
            if (existingEvent != null)
            {
                _events.Remove(existingEvent);
            }
        }
    }
}