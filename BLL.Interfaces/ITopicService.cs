using BLL.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ITopicService
    {
        Task<IEnumerable<TopicDto>> GetAllAsync();

        Task<TopicDto> GetByIdAsync(int id);

        Task CreateAsync(TopicDto topicDto);

        Task UpdateAsync(TopicDto topicDto);

        Task RemoveAsync(TopicDto topicsDto);

        Task CreateTopicWithImage(TopicDto topicDto, string fileName, string rootPath, byte[] image);
    }
}
