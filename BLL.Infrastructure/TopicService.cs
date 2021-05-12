using AutoMapper;
using BLL.Interfaces;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Infrastructure
{
    public class TopicService : ITopicService
    {
        private IUnitOfWork unit;
        private IMapper mapper;

        public TopicService(IUnitOfWork unitOfWork, IMapper automapper)
        {
            unit = unitOfWork;
            mapper = automapper;
        }
        public async Task CreateAsync(TopicModel topic)
        {
            var topicEntity = mapper.Map<TopicModel, Topic>(topic);

            await unit.Topics.CreateAsync(topicEntity);
            await unit.SaveChangesAsync();
        }

        public async Task<IEnumerable<TopicModel>> GetAllAsync()
        {
            var topics = await unit.Topics.GetAllAsync();

            return mapper.Map<IEnumerable<Topic>, IEnumerable<TopicModel>>(topics);
        }

        public async Task<TopicModel> GetByIdAsync(int id)
        {
            var topic = await unit.Topics.GetByIdAsync(id);

            return mapper.Map<Topic, TopicModel>(topic);
        }

        public async Task RemoveAsync(TopicModel topic)
        {
            var topicEntity = mapper.Map<TopicModel, Topic>(topic);

            unit.Topics.Remove(topicEntity);
            await unit.SaveChangesAsync();
        }

        public async Task UpdateAsync(TopicModel topic)
        {
            var topicEntity = mapper.Map<TopicModel, Topic>(topic);

            unit.Topics.Update(topicEntity);
            await unit.SaveChangesAsync();
        }
    }
}
