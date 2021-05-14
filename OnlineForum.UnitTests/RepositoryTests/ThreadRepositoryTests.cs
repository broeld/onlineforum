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
    public class ThreadRepositoryTests
    {
        [Test]
        public async Task ThreadRepository_GetAll_ReturnsAllValues ()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new ThreadRepository(context);

                var threads = await repo.GetAllAsync();

                Assert.AreEqual(4, threads.Count());
            }
        }

        [Test]
        public async Task ThreadRepository_GetById_ReturnsSingleValueAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new ThreadRepository(context);

                var thread = await repo.GetByIdAsync(151);

                Assert.AreEqual(151, thread.Id);
                Assert.AreEqual("Test thread 1", thread.Title);
                Assert.AreEqual("Some content", thread.Content);
                Assert.AreEqual(1001, thread.TopicId);
            }
        }

        [Test]
        public async Task ThreadRepository_CreateAsync_AddsValueInDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new ThreadRepository(context);
                var thread = new Thread()
                {
                    Id = 155,
                    Content = "Content",
                    Title = "Title",
                    IsOpen = true,
                    ThreadOpenedDate = DateTime.Now,
                    TopicId = 1001,
                    UserProfileId = 15
                };


                await repo.CreateAsync(thread);
                await context.SaveChangesAsync();

                Assert.AreEqual(5, context.Threads.Count());
            }
        }

        [Test]
        public async Task ThreadRepository_DeleteAsync_DeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new ThreadRepository(context);
                var thread = await repo.GetByIdAsync(151);

                repo.Remove(thread);
                await context.SaveChangesAsync();

                Assert.AreEqual(3, context.Threads.Count());
            }
        }

        [Test]
        public async Task ThreadRepository_UpdateAsync_UpdatesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new ThreadRepository(context);
                var thread = new Thread()
                {
                    Id = 151,
                    Content = "New Content",
                    Title = "New Title",
                    IsOpen = true,
                    ThreadOpenedDate = DateTime.Now,
                    TopicId = 1001,
                    UserProfileId = 15
                };

                repo.Update(thread);
                await context.SaveChangesAsync();

                var threadUpdated = await repo.GetByIdAsync(151);

                Assert.AreEqual(151, threadUpdated.Id);
                Assert.AreEqual("New Content", threadUpdated.Content);
                Assert.AreEqual("New Title", threadUpdated.Title);

            }
        }
    }
}
