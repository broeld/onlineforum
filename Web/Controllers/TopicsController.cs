using System.Collections.Generic;
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
    public class TopicsController : ControllerBase
    {
        private readonly ITopicService topicService;
        private readonly IMapper _mapper;

        public TopicsController(ITopicService service, IMapper mapper)
        {
            topicService = service;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TopicDetailModel>>> Get()
        {
            var topics = await topicService.GetAllAsync();
            var topicViewModels = _mapper.Map<IEnumerable<TopicModel>, List<TopicDetailModel>>(topics);

            return Ok(topicViewModels);
        }

        // GET: api/Topics/1
        [HttpGet("{id:int}")]
        public async Task<ActionResult<IEnumerable<TopicDetailModel>>> Get(int id)
        {
            var topic = await topicService.GetByIdAsync(id);

            if (topic == null)
                return BadRequest();

            var topicViewModel = _mapper.Map<TopicModel, TopicDetailModel>(topic);

            return Ok(topicViewModel);
        }


        // POST: api/Topics
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TopicCreateModel topic)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var topicModel = _mapper.Map<TopicCreateModel, TopicModel>(topic);

            await topicService.CreateAsync(topicModel);

            return Ok();
        }

        // PUT: api/Topics/5
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Put(int id, [FromBody] TopicDetailModel topicModel)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var topic = await topicService.GetByIdAsync(id);

            if (topic == null)
                return BadRequest();

            topic = _mapper.Map<TopicDetailModel, TopicModel>(topicModel);

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
