using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        private readonly IMapper _mapper;

        public UserController(IUserService service, IMapper mapper)
        {
            userService = service;
            _mapper = mapper;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UserViewModel>> Get(int id)
        {
            var user = await userService.GetUserDetail(id);

            if (user == null)
            {
                return BadRequest();
            }

            var userViewModel = _mapper.Map<UserModel, UserViewModel>(user);

            return Ok(userViewModel);
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
    }
}
