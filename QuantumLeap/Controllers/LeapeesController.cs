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

        [HttpPost]
        public ActionResult CreateLeapee(CreateLeapeeRequest createRequest)
        {
            var newLeapee = _leapeeRepository.AddLeapee(createRequest.Name);

            return Created($"api/leapees/{newLeapee.Id}", newLeapee);
        }

        [HttpPut("{id}")]
        public ActionResult UpdateLeapee(Leapee leapeeToUpdate)
        {
            var updatedLeapee = _leapeeRepository.UpdateLeapee(leapeeToUpdate);

            return Ok(updatedLeapee);
        }

        [HttpDelete("{leapeeId}")]
        public ActionResult DeleteLeapee(int leapeeId)
        {
            _leapeeRepository.DeleteLeapee(leapeeId);

            return Ok();
        }
    }
}