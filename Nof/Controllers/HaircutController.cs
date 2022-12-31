using Microsoft.AspNetCore.Mvc;
using Nof.Helpers;
using Nof.Model;
using Nof.Models;
using Nof.Services;


namespace Nof.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HaircutController : Controller
    {
        private readonly IHaircutService _haircutService;
        public HaircutController(IHaircutService haircutService)
        {
            _haircutService = haircutService;
        }
        [Authorize]
        [HttpGet("getAllAppointments")]
        public IActionResult GetAllAppointments()
        {
            var results = _haircutService.GetAllAppointments();
            return Ok(results);
        }

        [Authorize]
        [HttpGet("getAppointmentByUserId/{userId}")]
        public IActionResult GetAppointmentByUserId(int userId)
        {
            var user = (User)Request.HttpContext.Items["User"];
            if(user == null || user.Id != userId)
            {
                return BadRequest("not authorized");
            }
            var results = _haircutService.GetAppointmentByUserId(userId);
            return Ok(results);
        }

        [Authorize]
        [HttpGet("deleteAppointment/{userId}")]
        public IActionResult DeleteAppointment(int userId)
        {
            var user = (User)Request.HttpContext.Items["User"];
            if (user == null || user.Id != userId)
            {
                return BadRequest("not authorized");
            }
            var results = _haircutService.DeleteAppointment(userId);
            return Ok(results);
        }

        [Authorize]
        [HttpPost("addOrChangeAppointment")]
        public IActionResult AddOrChangeAppointment([FromBody] HaircutRecored haircutRecored)
        {
            var user = (User)Request.HttpContext.Items["User"];
            if (user == null || user.Id != haircutRecored.User.Id)
            {
                return BadRequest("not authorized");
            }
            var results = _haircutService.AddOrChangeAppointment(haircutRecored);
            return Ok(results);
        }
    }
}
