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
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadService threadService;

        public ThreadsController(IThreadService service)
        {
            threadService = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThreadModel>>> Get()
        {
            var threads = await threadService.GetAllAsync();

            return Ok(threads);
        }

        // GET: api/Threads/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ThreadModel>> Get(int id)
        {
            var thread = await threadService.GetByIdAsync(id);

            if (thread == null)
                return BadRequest();

            return Ok(thread);
        }

        //GET api/topics/4/threads
        [HttpGet]
        [Route("~/api/topics/{topicId:int}/threads")]
        public async Task<ActionResult<IEnumerable<ThreadModel>>> GetThreadsByTopicId(int topicId)
        {
            var threads = await threadService.GetThreadsByTopicId(topicId);

            if (threads == null)
                return BadRequest();

            return Ok(threads);
        }

        // POST: api/Threads
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] ThreadModel thread)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            await threadService.CreateAsync(thread);

            return Ok();
        }

        [Authorize]
        // DELETE: api/Threads/5
        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var thread = await threadService.GetByIdAsync(id);

            if (thread == null)
                return BadRequest();

            await threadService.RemoveAsync(thread);

            return Ok();
        }


        [Authorize(Roles = "Admin")]
        [HttpPut("deactivate/{threadId}")]
        public async Task<IActionResult> Deactivate(int threadId)
        {
            var isSuccessful = await threadService.Deactivate(threadId);

            if (!isSuccessful)
                return BadRequest();

            return Ok();
        }
    }
}
