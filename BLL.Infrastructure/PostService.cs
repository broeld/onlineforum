using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class PostService : IPostService
    {
        private IUnitOfWork unit;
        private IMapper mapper;

        public PostService(IUnitOfWork unitOfWork, IMapper automapper)
        {
            unit = unitOfWork;
            mapper = automapper;
        }

        public async Task CreateAsync(PostModel post)
        {
            if (post == null)
            {
                throw new Exception("Post is null");
            }

            var postEntity = mapper.Map<PostModel, Post>(post);
            var author = await unit.UserProfiles.GetByIdAsync(post.UserProfileId);
            author.Rating++;

            await unit.Posts.CreateAsync(postEntity);
            await unit.SaveChangesAsync();

            unit.UserProfiles.Update(author);
            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<PostModel>> GetAllAsync()
        {
            var posts = await unit.Posts.GetAllAsync();

            return mapper.Map<IEnumerable<Post>, IEnumerable<PostModel>>(posts);
        }

        public async Task<PostModel> GetByIdAsync(int id)
        {
            var post = await unit.Posts.GetByIdAsync(id);

            if (post != null)
            {
                return mapper.Map<Post, PostModel>(post);
            }

            throw new Exception("Query result is null");
        }

        public async Task<IEnumerable<PostModel>> GetPostsByThreadId(int threadId)
        {
            var posts = await unit.Posts.GetAllAsync();
            var postsByThread = posts.Where(p => p.ThreadId == threadId);

            return mapper.Map<IEnumerable<Post>, IEnumerable<PostModel>>(postsByThread);
        }

        public async Task RemoveAsync(PostModel post)
        {
            var users = await unit.UserProfiles.GetWithIncludeAsync();
            var user = users.FirstOrDefault(u => u.Id == post.UserProfileId);

            user.Rating--;

            var postEntity = await unit.Posts.GetByIdAsync(post.Id);
            var replies = postEntity.Replies;

            foreach(var reply in replies)
            {
                reply.RepliedPostId = null;
                unit.Posts.Update(reply);
            }

            await unit.SaveChangesAsync();

            unit.Posts.Remove(postEntity);
            unit.UserProfiles.Update(user);
            await unit.SaveChangesAsync();
      
        }

        public async Task UpdateAsync(PostModel post)
        {
            if (post == null)
            {
                throw new Exception("Post is null");
            }

            var postEntity = mapper.Map<PostModel, Post>(post);

            unit.Posts.Update(postEntity);
            await unit.SaveChangesAsync();
        }
    }
}
