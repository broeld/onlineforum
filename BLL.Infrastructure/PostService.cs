using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class PostService : BaseService, IPostService
    {
        public Task CreateAsync(PostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PostDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<PostDto>> GetPostsByThreadId(int threadId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(PostDto postDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(PostDto postDto)
        {
            throw new NotImplementedException();
        }
    }
}
