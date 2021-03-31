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
    public class ThreadService : IThreadService
    {
        private IUnitOfWork unit;
        private IMapper mapper;

        public ThreadService(IUnitOfWork unitOfWork, IMapper automapper)
        {
            unit = unitOfWork;
            mapper = automapper;
        }

        public async Task CreateAsync(ThreadModel thread)
        {
            if (thread == null) throw new Exception("Thread is null");

            var threadEntity = mapper.Map<ThreadModel, Thread>(thread);

            await unit.Threads.CreateAsync(threadEntity);
            await unit.SaveChangesAsync();
        }

        public async Task<bool> Deactivate(int threadId)
        {
            var thread = await unit.Threads.GetByIdAsync(threadId);

            thread.IsOpen = false;
            thread.ThreadClosedDate = DateTime.Now;

            unit.Threads.Update(thread);
            await unit.SaveChangesAsync();

            return true;
        }

        public async Task<IEnumerable<ThreadModel>> GetAllAsync()
        {
            var threads = await unit.Threads.GetAllAsync();

            return mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadModel>>(threads);
        }

        public async Task<ThreadModel> GetByIdAsync(int id)
        {
            var thread = await unit.Threads.GetByIdAsync(id);
            if (thread != null)
            {
                var threadModel = mapper.Map<Thread, ThreadModel>(thread);
                return threadModel;
            }

            throw new Exception("Thread is null");
        }

        public async Task<IEnumerable<ThreadModel>> GetThreadsByTopicId(int topicId)
        {
            var threads = await unit.Threads.GetAllAsync();
            var threadsByTopic = threads.Where(t => t.TopicId == topicId);

            return mapper.Map<IEnumerable<Thread>, IEnumerable<ThreadModel>>(threadsByTopic);

        }

        public async Task RemoveAsync(ThreadModel thread)
        {
            var threadEntity = mapper.Map<ThreadModel, Thread>(thread);

            unit.Threads.Remove(threadEntity);
            await unit.SaveChangesAsync();
        }

        public async Task UpdateAsync(ThreadModel thread)
        {
            var threadEntity = mapper.Map<ThreadModel, Thread>(thread);

            unit.Threads.Update(threadEntity);
            await unit.SaveChangesAsync();
        }
    }
}
