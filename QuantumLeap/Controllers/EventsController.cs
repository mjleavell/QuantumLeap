using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuantumLeap.Data;

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
    }
}