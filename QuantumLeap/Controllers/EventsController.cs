using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantumLeap.Data;
using QuantumLeap.Models;

namespace QuantumLeap.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : ControllerBase
    {
        readonly EventRepository _eventRepository;

        public EventsController()
        {
            _eventRepository = new EventRepository();
        }

        [HttpGet]
        public ActionResult GetAllEvents()
        {
            var events = _eventRepository.GetEvents();

            return Ok(events);
        }

        [HttpPost]
        public ActionResult CreateEvent(CreateEventRequest createRequest)
        {
            var newEvent = _eventRepository.AddEvent(createRequest.Name, createRequest.EventDate, createRequest.IsCorrected);

            return Created($"api/events/{newEvent.Id}", newEvent);
        }

        [HttpPut("{eventId}")]
        public ActionResult UpdateEvent(Event eventToUpdate)
        {
            var updatedEvent = _eventRepository.UpdateEvent(eventToUpdate);

            return Ok(updatedEvent);
        }

        [HttpDelete("{eventId}")]
        public ActionResult DeleteEvent(int eventId)
        {
            _eventRepository.DeleteEvent(eventId);

            return Ok();
        }
    }
}