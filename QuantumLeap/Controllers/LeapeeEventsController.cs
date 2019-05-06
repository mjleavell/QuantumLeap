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
    public class LeapeeEventsController : ControllerBase
    {
        readonly LeapeeEventsRepository _leapeeEventsRepository;

        public LeapeeEventsController()
        {
            _leapeeEventsRepository = new LeapeeEventsRepository();
        }

        [HttpGet]
        public ActionResult GetAllLeapeesEvents()
        {
            var leapeesEvents = _leapeeEventsRepository.GetAll();

            return Ok(leapeesEvents);
        }
    }
}