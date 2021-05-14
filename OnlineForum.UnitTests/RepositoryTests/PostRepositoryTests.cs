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
    public class PostRepositoryTests
    {
        [Test]
        public async Task PostRepository_GetAll_ReturnsAllValuesAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new PostRepository(context);

                var posts = await repo.GetAllAsync();

                Assert.AreEqual(6, posts.Count());
            }
        }

        [Test]
        public async Task PostRepository_GetById_ReturnsSingleValueAsync()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new PostRepository(context);

                var post = await repo.GetByIdAsync(201);

                Assert.AreEqual(201, post.Id);
                Assert.AreEqual("First reply to thread", post.Content);
                Assert.AreEqual(151, post.ThreadId);
                Assert.AreEqual(15, post.UserProfileId);
            }
        }

        [Test]
        public async Task PostRepository_CreateAsync_AddsValueInDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new PostRepository(context);
                var post = new Post() { Id = 215, UserProfileId = 15, Content = "Test", ThreadId = 151, PostDate = DateTime.Now };

                await repo.CreateAsync(post);
                await context.SaveChangesAsync();

                Assert.AreEqual(7, context.Posts.Count());
            }
        }

        [Test]
        public async Task PostRepository_DeleteAsync_DeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new PostRepository(context);
                var post = await repo.GetByIdAsync(201);

                repo.Remove(post);
                await context.SaveChangesAsync();

                Assert.AreEqual(5, context.Posts.Count());
            }
        }

        [Test]
        public async Task PostRepository_UpdateAsync_DeletesValueFromDatabase()
        {
            using (var context = new ForumDbContext(UnitTestsHelper.GetUnitTestDbOptions()))
            {
                var repo = new PostRepository(context);
                var post = new Post() { Id = 201, UserProfileId = 15, Content = "Test Content", ThreadId = 152, PostDate = DateTime.Now };

                repo.Update(post);
                await context.SaveChangesAsync();

                var postUpdated = await repo.GetByIdAsync(201);

                Assert.AreEqual(201, postUpdated.Id);
                Assert.AreEqual("Test Content", postUpdated.Content);
                Assert.AreEqual(152, postUpdated.ThreadId);
            }
        }
    }
}
