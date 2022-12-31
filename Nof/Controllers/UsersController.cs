using Microsoft.AspNetCore.Mvc;
using Nof.Helpers;
using Nof.Model;
using Nof.Models;
using Nof.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nof.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Username or password is incorrect" });

            return Ok(response);
        }

        [HttpPost("register")]
        public IActionResult RegisterUser([FromBody] User user)
        {
            var returnKod = _userService.RegisterUser(user);
            if(returnKod > 0)
            {
                var userAutReq = new AuthenticateRequest() { Password = user.Password, Username = user.Username };
                return Authenticate(userAutReq);
            }else if(returnKod == 0)
            {
                return BadRequest(new { message = "Username Already Exist" });
            }
            else
            {
                return BadRequest(new { message = "Something went wrong..." });
            }

        }

    }

}
