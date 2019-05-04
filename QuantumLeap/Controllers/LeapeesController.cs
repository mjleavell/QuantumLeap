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
    public class LeapeesController : ControllerBase
    {
        readonly LeapeeRepository _leapeeRepository;

        public LeapeesController()
        {
            _leapeeRepository = new LeapeeRepository();
        }

        [HttpGet]
        public ActionResult GetAllLeapees()
        {
            var leapees = _leapeeRepository.GetLeapees();

            return Ok(leapees);
        }
    }
}