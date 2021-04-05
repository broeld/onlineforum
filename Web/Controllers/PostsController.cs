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
    public class PostsController : ControllerBase
    {
        private readonly IPostService postService;
        private readonly IUserService userService;

        public PostsController(IPostService pService, IUserService uService)
        {
            postService = pService;
            userService = uService;
        }

        // GET: api/Posts
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PostModel>>> Get()
        {
            var posts = await postService.GetAllAsync();

            return Ok(posts);
        }

        // GET: api/Posts/5
        [HttpGet("{id:int}")]
        public async Task<ActionResult<PostModel>> Get(int id)
        {
            var post = await postService.GetByIdAsync(id);

            if (post == null)
                return BadRequest();

            return Ok(post);
        }


        //GET: api/threads/4/posts
        [HttpGet]
        [Route("~/api/threads/{threadId}/posts")]
        public async Task<ActionResult<IEnumerable<PostModel>>> GetPostsByThreadId(int threadId)
        {
            var posts = await postService.GetPostsByThreadId(threadId);

            if (posts == null)
                return BadRequest();

            return Ok(posts);
        }


        // POST: api/Posts
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post([FromBody] PostModel post)
        {
            await postService.CreateAsync(post);

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
