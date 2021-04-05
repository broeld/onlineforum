using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private IUserService userService;

        // private IHostingEnvironment hostingEnv;

        public UserController(IUserService service)
        {
            //hostingEnv = hostingEnvironment;
            userService = service;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserModel>> Get(int id)
        {
            var user = await userService.GetUserDetail(id);

            if (user == null)
            {
                return BadRequest();
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("deactivate/{userId}")]
        public async Task<IActionResult> Deactivate(int userId)
        {
            var isSuccessful = await userService.Deactivate(userId);

            if (!isSuccessful)
                return BadRequest();

            return Ok();
        }

        [Authorize]
        [HttpPut]
        [Route("upload-image/{userId}")]
        //public async Task<IActionResult> UploadImage([FromRoute] int userId, [FromForm] ImageForm userImageForm)
        public async Task<IActionResult> UploadImage([FromRoute] int userId)
        {
            return Ok();
        }
    }
}
