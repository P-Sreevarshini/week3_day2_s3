// Controllers/EventController.cs
using Microsoft.AspNetCore.Mvc;
using dotnetapp.Models;
using dotnetapp.Services;
using System.Collections.Generic;

namespace dotnetapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Event>> GetAllEvents()
        {
            var events = _eventService.GetAllEvents();
            if (events == null)
            {
                return NoContent(); // HTTP 204
            }
            return Ok(events); // HTTP 200
        }

        [HttpGet("{eventId}")]
        public ActionResult<Event> GetEventById(int eventId)
        {
            var @event = _eventService.GetEventById(eventId);
            if (@event == null)
            {
                return NotFound(); // HTTP 404
            }
            return Ok(@event); // HTTP 200
        }


        [HttpPost]
        public ActionResult<Event> CreateEvent(Event newEvent)
        {
            if (newEvent == null)
            {
                return BadRequest(); // HTTP 400
            }
            _eventService.AddEvent(newEvent);
            return CreatedAtAction(nameof(GetEventById), new { eventId = newEvent.EventId }, newEvent); // HTTP 201
        }

        [HttpPut("{eventId}")]
        public ActionResult UpdateEvent(int eventId, Event updatedEvent)
        {
            var existingEvent = _eventService.GetEventById(eventId);
            if (existingEvent == null)
            {
                return NotFound(); // HTTP 404
            }
            _eventService.UpdateEvent(eventId, updatedEvent);
            return NoContent(); // HTTP 204
        }

        [HttpDelete("{eventId}")]
        public ActionResult DeleteEvent(int eventId)
        {
            var existingEvent = _eventService.GetEventById(eventId);
            if (existingEvent == null)
            {
                return NotFound(); // HTTP 404
            }
            _eventService.DeleteEvent(eventId);
            return NoContent(); // HTTP 204
        }
    }
}