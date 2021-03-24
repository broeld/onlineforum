using BLL.DTO;
using BLL.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class ThreadService : BaseService, IThreadService
    {
        public Task CreateAsync(ThreadDto threadDto)
        {
            throw new NotImplementedException();
        }

        public Task<bool> Deactivate(int threadId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ThreadDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ThreadDto> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ThreadDto>> GetThreadsByTopicId(int topicId)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(ThreadDto threadDto)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(ThreadDto threadDto)
        {
            throw new NotImplementedException();
        }
    }
}
