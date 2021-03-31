using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<PostModel>> GetAllAsync();

        Task<PostModel> GetByIdAsync(int id);

        Task CreateAsync(PostModel post);

        Task UpdateAsync(PostModel post);

        Task RemoveAsync(PostModel post);

        Task<IEnumerable<PostModel>> GetPostsByThreadId(int threadId);
    }
}
