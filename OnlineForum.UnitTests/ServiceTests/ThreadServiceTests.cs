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
    public class ThreadServiceTests
    {

        private IEnumerable<Thread> GetTestThreadEntites()
        {
            return new List<Thread>()
            {
                new Thread { Id = 1, Content = "Content", Title = "First", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 20, UserProfileId = 10 },
                new Thread { Id = 2, Content = "Content", Title = "Second", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 20, UserProfileId = 11 },
                new Thread { Id = 3, Content = "Content", Title = "Third", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 21, UserProfileId = 12 },
                new Thread { Id = 4, Content = "Content", Title = "Fourth", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 21, UserProfileId = 11 },
                new Thread { Id = 5, Content = "Content", Title = "Fifth", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 22, UserProfileId = 10 },
            };
        }

        private IEnumerable<ThreadModel> GetTestThreadModels()
        {
            return new List<ThreadModel>()
            {
                new ThreadModel { Id = 1, Content = "Content", Title = "First", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 20, UserProfileId = 10 },
                new ThreadModel { Id = 2, Content = "Content", Title = "Second", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 20, UserProfileId = 11 },
                new ThreadModel { Id = 3, Content = "Content", Title = "Third", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 21, UserProfileId = 12 },
                new ThreadModel { Id = 4, Content = "Content", Title = "Fourth", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 21, UserProfileId = 11 },
                new ThreadModel { Id = 5, Content = "Content", Title = "Fifth", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 22, UserProfileId = 10 },
            };
        }

        [Test]
        public async Task ThreadService_GetAllAsync_ReturnsAllValues()
        {
            var expected = GetTestThreadModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Threads.GetAllAsync())
                .ReturnsAsync(GetTestThreadEntites());

            var service = new ThreadService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var threads = await service.GetAllAsync();
            var actual = threads.ToList();

            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Content, actual[i].Content);
                Assert.AreEqual(expected[i].Title, actual[i].Title);
                Assert.AreEqual(expected[i].TopicId, actual[i].TopicId);
                Assert.AreEqual(expected[i].UserProfileId, actual[i].UserProfileId);
            }

        }

        [Test]
        public async Task ThreadService_GetByIdAsync_ReturnsSingleValue()
        {
            var expected = GetTestThreadModels().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Threads.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestThreadEntites().First);

            var service = new ThreadService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var actual = await service.GetByIdAsync(1);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Content, actual.Content);
            Assert.AreEqual(expected.Title, actual.Title);
            Assert.AreEqual(expected.TopicId, actual.TopicId);
            Assert.AreEqual(expected.UserProfileId, actual.UserProfileId);
        }

        [TestCase(20)]
        [TestCase(21)]
        [TestCase(22)]
        public async Task ThreadService_GetThreadByTopicId_ReturnsThreadaByTopicId(int topicId)
        {
            var expected = GetTestThreadModels().Where(p => p.TopicId == topicId).ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Threads.GetAllAsync())
                .ReturnsAsync(GetTestThreadEntites);

            var service = new ThreadService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var threads = await service.GetThreadsByTopicId(topicId);
            var actual = threads.ToList();

            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Content, actual[i].Content);
                Assert.AreEqual(expected[i].Title, actual[i].Title);
                Assert.AreEqual(expected[i].TopicId, actual[i].TopicId);
                Assert.AreEqual(expected[i].UserProfileId, actual[i].UserProfileId);
            }
        }

        [Test]
        public async Task ThreadService_RemoveAsync_RemoveFromDatabase()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Threads.Remove(It.IsAny<Thread>()));

            var service = new ThreadService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var thread = GetTestThreadModels().First();

            await service.RemoveAsync(thread);

            mockUnitOfWork.Verify(x => x.Threads.Remove(It.Is<Thread>(t => t.Id == thread.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task ThreadService_UpdateAsync_UpdateValueInDatabase()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(m => m.Threads.Update(It.IsAny<Thread>()));

            var service = new ThreadService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var thread = new ThreadModel { Id = 4, Content = "New Content", Title = "Fourth Updated", IsOpen = true, ThreadOpenedDate = DateTime.Now, TopicId = 21, UserProfileId = 11 };

            await service.UpdateAsync(thread);

            mockUnitOfWork.Verify(x => x.Threads.Update(It.Is<Thread>(t => t.Id == thread.Id && t.Content == thread.Content)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);

        }

        [Test]
        public async Task ThreadService_Deactivate_DeactivateProfileReturnsTrue()
        {
            var thread = GetTestThreadModels().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Threads.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestThreadEntites().First);
            mockUnitOfWork.Setup(m => m.Threads.Update(It.IsAny<Thread>()));

            var service = new ThreadService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            await service.Deactivate(thread.Id);

            var actual = await service.GetByIdAsync(thread.Id);

            Assert.AreEqual(1, actual.Id);
            Assert.AreEqual(false, actual.IsOpen);
            mockUnitOfWork.Verify(x => x.Threads.Update(It.Is<Thread>(t => t.Id == thread.Id && t.Content == thread.Content)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
