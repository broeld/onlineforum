using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IThreadService
    {
        Task<IEnumerable<ThreadModel>> GetAllAsync();

        Task<ThreadModel> GetByIdAsync(int id);

        Task CreateAsync(ThreadModel thread);

        Task UpdateAsync(ThreadModel thread);

        Task RemoveAsync(ThreadModel thread);

        Task<IEnumerable<ThreadModel>> GetThreadsByTopicId(int topicId);

        Task<bool> Deactivate(int threadId);
    }
}
