using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class TopicService : BaseService, ITopicService
    {
        public Task CreateAsync(TopicDto topicDto)
        {
            throw new NotImplementedException();
        }

        public Task CreateTopicWithImage(TopicDto topicDto, string fileName, string rootPath, byte[] image)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<TopicDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<TopicDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TopicDto topicsDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TopicDto topicDto)
        {
            throw new NotImplementedException();
        }
    }
}
