using DAL.Domain;
using DAL.Infrastructure;
using DAL.Infrastructure.Repositories;
using NUnit.Framework;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineForum.UnitTests.RepositoryTests
{
    [TestFixture]
    public class TopicRepositoryTests
    {
        [Test]
        public async Task TopicRepository_GetAll_ReturnsAllValuesAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new TopicRepository(context);

                var topics = await repo.GetAllAsync();

                Assert.AreEqual(3, topics.Count());
            }
        }

        [Test]
        public async Task TopicRepository_GetById_ReturnsSingleValueAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new TopicRepository(context);

                var topic = await repo.GetByIdAsync(1001);

                Assert.AreEqual(1001, topic.Id);
                Assert.AreEqual("Python", topic.Title);
                Assert.AreEqual("Python is the best programming language ever", topic.Description);
            }
        }

        [Test]
        public async Task TopicRepository_CreateAsync_AddsValueInDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new TopicRepository(context);
                var topic = new Topic() { Id = 1005, Title = "New title", Description = "Desc" };

                await repo.CreateAsync(topic);
                await context.SaveChangesAsync();

                Assert.AreEqual(4, context.Topics.Count());
                
            }
        }

        [Test]
        public async Task TopicRepository_AsyncDeleteAsync_DeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new TopicRepository(context);
                var topic = await repo.GetByIdAsync(1002);

                repo.Remove(topic);
                await context.SaveChangesAsync();

                Assert.AreEqual(2, context.Topics.Count());

            }
        }

        [Test]
        public async Task TopicRepository_UpdateAsync_DeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new TopicRepository(context);
                var topic = new Topic()
                {
                    Id = 1001,
                    Title = "PythonTopic",
                    Description = "Python is the best"
                };

                repo.Update(topic);
                await context.SaveChangesAsync();

                var topicUpdated = await repo.GetByIdAsync(1001);

                Assert.AreEqual(1001, topicUpdated.Id);
                Assert.AreEqual("PythonTopic", topicUpdated.Title);
                Assert.AreEqual("Python is the best", topicUpdated.Description);

            }
        }
    }
}
