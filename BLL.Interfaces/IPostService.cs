using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostDto>> GetAllAsync();

        Task<PostDto> GetByIdAsync(int id);

        Task CreateAsync(PostDto postDto);

        Task UpdateAsync(PostDto postDto);

        Task RemoveAsync(PostDto postDto);

        Task<IEnumerable<PostDto>> GetPostsByThreadId(int threadId);
    }
}
