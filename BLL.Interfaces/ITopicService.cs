using BLL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicModel>> GetAllAsync();

        Task<TopicModel> GetByIdAsync(int id);

        Task CreateAsync(TopicModel topicDto);

        Task UpdateAsync(TopicModel topicDto);

        Task RemoveAsync(TopicModel topicsDto);
    }
}
