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
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService topicService;
        // private readonly IHostingEnvironment hostingEnv;

        public TopicsController(ITopicService service)
        {
            topicService = service;
            // hostingEnv = hostingEnvironment;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicModel>>> Get()
        {
            var topics = await topicService.GetAllAsync();

            return Ok(topics);
        }

        // GET: api/Topics/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<TopicModel>>> Get(int id)
        {
            var topic = await topicService.GetByIdAsync(id);

            if (topic == null)
                return BadRequest();

            return Ok(topic);
        }


        // POST: api/Topics
        [Authorize(Roles = "Admin")]
        [HttpPost]
        // public async Task<IActionResult> Post([FromForm] CreateTopicForm topicForm)
        public async Task<IActionResult> Post()
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok();
        }

        // PUT: api/Topics/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] TopicModel topicModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var topic = await topicService.GetByIdAsync(id);

            if (topic == null)
                return BadRequest();

            topic = topicModel;

            await topicService.UpdateAsync(topic);
            return Ok();
        }

        // DELETE: api/Topics/5
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var topic = await topicService.GetByIdAsync(id);

            if (topic == null)
                return BadRequest();

            await topicService.RemoveAsync(topic);

            return Ok();
        }
    }
}
