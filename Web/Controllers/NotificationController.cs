using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;

        public NotificationController(INotificationService service)
        {
            notificationService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationModel>>> GetAll()
        {
            return Ok(await notificationService.GetAllAsync());
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NotificationModel notificationModel)
        {
            if (notificationModel == null)
                return BadRequest();

            await notificationService.CreateAsync(notificationModel);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var notification = await notificationService.GetByIdAsync(id);

            if (notification == null)
                return BadRequest();

            await notificationService.RemoveAsync(notification);

            return Ok();
        }
    }
}
