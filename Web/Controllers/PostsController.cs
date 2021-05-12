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
    public class PostsController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IMapper _mapper;

        public PostsController(IPostService pService, IMapper mapper)
        {
            postService = pService;
            _mapper = mapper;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostDetailModel>>> Get()
        {
            var posts = await postService.GetAllAsync();
            var postViewModels = _mapper.Map<IEnumerable<PostModel>, List<PostDetailModel>>(posts);

            return Ok(postViewModels);
        }

        // GET: api/Posts/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostDetailModel>> Get(int id)
        {
            var post = await postService.GetByIdAsync(id);

            if (post == null)
                return BadRequest();

            var postViewModel = _mapper.Map<PostModel, PostDetailModel>(post);

            return Ok(postViewModel);
        }


        //GET: api/threads/4/posts
        [HttpGet]
        [Route("~/api/threads/{threadId}/posts")]
        public async Task<ActionResult<IEnumerable<PostDetailModel>>> GetPostsByThreadId(int threadId)
        {
            var posts = await postService.GetPostsByThreadId(threadId);

            if (posts == null)
                return BadRequest();

            var postViewModels = _mapper.Map<IEnumerable<PostModel>, List<PostDetailModel>>(posts);

            return Ok(postViewModels);
        }


        // POST: api/Posts
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] PostCreateModel post)
        {
            var postModel = _mapper.Map<PostCreateModel, PostModel>(post);
            await postService.CreateAsync(postModel);

            return Ok();
        }


        //DELETE: api/Posts/5
        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var postDto = await postService.GetByIdAsync(id);

            if (postDto == null)
                return BadRequest();

            await postService.RemoveAsync(postDto);

            return Ok();
        }
    }
}
