using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService notificationService;
        private readonly IMapper _mapper;

        public NotificationController(INotificationService service, IMapper mapper)
        {         
            notificationService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<NotificationViewModel>>> GetAll()
        {
            var notifications = await notificationService.GetAllAsync();

            var notificationViewModels = _mapper.Map<IEnumerable<NotificationModel>,
                List<NotificationViewModel>>(notifications);

            return Ok(notificationViewModels);

        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] NotificationCreateModel notificationModel)
        {
            if (notificationModel == null)
                return BadRequest();

            var notification = _mapper.Map<NotificationCreateModel, NotificationModel>(notificationModel);

            await notificationService.CreateAsync(notification);

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
