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
    public class LeapersController : ControllerBase
    {
        readonly LeaperRepository _leaperRepository;

        public LeapersController()
        {
            _leaperRepository = new LeaperRepository();
        }

        [HttpGet]
        public ActionResult GetLeapers()
        {
            var allLeapers = _leaperRepository.GetLeapers();
            return Ok(allLeapers);
        }
    }
}