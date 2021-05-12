using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IConfiguration configuration;

        public AccountController(IUserService service, IConfiguration conf)
        {
            userService = service;
            configuration = conf;
        }

        [HttpPost("login")]
        public async Task<ActionResult<SignedInUserModel>> Login([FromBody]LoginModel loginModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var signedInUser = await userService.SignInAsync(loginModel, configuration["Tokens:Key"],
                    int.Parse(configuration["Tokens:ExpiryMinutes"]),
                    configuration["Tokens:Audience"], configuration["Tokens:Issuer"]);

            return Ok(signedInUser);
        }

        [HttpPost("registration")]
        public async Task<ActionResult<SignedInUserModel>> Register([FromBody] RegistrationModel registrationModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest();
            }

            var signedInUser = await userService.SignUpAsync(registrationModel, configuration["Tokens:Key"],
                    int.Parse(configuration["Tokens:ExpiryMinutes"]),
                    configuration["Tokens:Audience"], configuration["Tokens:Issuer"]);

            return Ok(signedInUser);
        }

        [HttpGet("logout")]
        public async Task<IActionResult> Logout()
        {
            await userService.SignOutAsync();

            return Ok();
        }

        [HttpGet("isadmin")]
        public async Task<ActionResult<bool>> IsAdmin(int userId)
        {
            var isAdmin = await userService.IsInRoleAsync(userId, "Admin");

            return Ok(isAdmin);
        }
    }
}
