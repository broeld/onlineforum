using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Web.ViewModels;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ThreadsController : ControllerBase
    {
        private readonly IThreadService threadService;
        private readonly IMapper _mapper;

        public ThreadsController(IThreadService service, IMapper mapper)
        {
            threadService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ThreadDisplayViewModel>>> Get()
        {
            var threads = await threadService.GetAllAsync();

            var threadViewModels = _mapper.Map<IEnumerable<ThreadModel>, List<ThreadDisplayViewModel>>(threads);

            return Ok(threadViewModels);
        }

        // GET: api/Threads/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ThreadDisplayViewModel>> Get(int id)
        {
            var thread = await threadService.GetByIdAsync(id);

            if (thread == null)
                return BadRequest();

            var threadViewModel = _mapper.Map<ThreadModel, ThreadDisplayViewModel>(thread);

            return Ok(threadViewModel);
        }

        //GET api/topics/4/threads
        [HttpGet]
        [Route("~/api/topics/{topicId:int}/threads")]
        public async Task<ActionResult<IEnumerable<ThreadDisplayViewModel>>> GetThreadsByTopicId(int topicId)
        {
            var threads = await threadService.GetThreadsByTopicId(topicId);

            if (threads == null)
                return BadRequest();

            var threadViewModels = _mapper.Map<IEnumerable<ThreadModel>, List<ThreadDisplayViewModel>>(threads);

            return Ok(threadViewModels);
        }

        // POST: api/Threads
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> Post([FromBody] ThreadCreateModel thread)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var threadModel = _mapper.Map<ThreadCreateModel, ThreadModel>(thread);

            await threadService.CreateAsync(threadModel);

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
