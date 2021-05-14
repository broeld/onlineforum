using BLL.Infrastructure;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineForum.UnitTests.ServiceTests
{
    public class TopicServiceTests
    {
        private IEnumerable<Topic> GetTestTopicEntities()
        {
            return new List<Topic> {
            new Topic
            {
                Id = 1,
                Title = "Python",
                Description = "Python is the best programming language ever"
            },
            new Topic
            { 
                Id = 2,
                Title = "PythonTwo",
                Description = "Python is the best programming language ever"
            }
            };
        }

        private IEnumerable<TopicModel> GetTestTopicModels()
        {
            return new List<TopicModel> {
            new TopicModel
            {
                Id = 1,
                Title = "Python",
                Description = "Python is the best programming language ever"
            },
            new TopicModel
            {
                Id = 2,
                Title = "PythonTwo",
                Description = "Python is the best programming language ever"
            }
            };
        }

        [Test]
        public async Task TopicService_GetAllAsync_RetutnsAllTopics()
        {
            var expected = GetTestTopicModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Topics.GetAllAsync())
                .ReturnsAsync(GetTestTopicEntities());

            var service = new TopicService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var topics = await service.GetAllAsync();
            var actual = topics.ToList();

            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Description, actual[i].Description);
                Assert.AreEqual(expected[i].Title, actual[i].Title);
            }

        }

        [Test]
        public async Task TopicService_GetByIdAsync_RetutnsAllTopics()
        {
            var expected = GetTestTopicModels().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Topics.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestTopicEntities().First);

            var service = new TopicService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var actual = await service.GetByIdAsync(1);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Description, actual.Description);
            Assert.AreEqual(expected.Title, actual.Title);

        }

        [Test]
        public async Task TopicService_RemoveAsync_RetutnsAllTopics()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Topics.Remove(It.IsAny<Topic>()));

            var service = new TopicService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var topic = GetTestTopicModels().First();

            await service.RemoveAsync(topic);

            mockUnitOfWork.Verify(x => x.Topics.Remove(It.Is<Topic>(t => t.Id == topic.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task TopicService_UpdateAsync_RetutnsAllTopics()
        {

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Topics.Update(It.IsAny<Topic>()));

            var service = new TopicService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var topic = GetTestTopicModels().First();

            await service.UpdateAsync(topic);

            mockUnitOfWork.Verify(x => x.Topics.Update(It.Is<Topic>(t => t.Id == topic.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

    }
}
