using BLL.Infrastructure;
using BLL.Models;
using DAL.Domain;
using DAL.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineForum.UnitTests.ServiceTests
{
    public class PostServiceTests
    {

        private IEnumerable<Post> GetTestPostEntities()
        {
            return new List<Post>()
            {
                new Post {Id = 1, Content = "First Post", PostDate = DateTime.Now, ThreadId = 10, UserProfileId = 20 },
                new Post {Id = 2, Content = "Second Post", PostDate = DateTime.Now, ThreadId = 10, UserProfileId = 20 },
                new Post {Id = 3, Content = "Third Post", PostDate = DateTime.Now, ThreadId = 11, UserProfileId = 21 },
                new Post {Id = 4, Content = "Fourth Post", PostDate = DateTime.Now, ThreadId = 12, UserProfileId = 21 },
                new Post {Id = 5, Content = "Fifth Post", PostDate = DateTime.Now, ThreadId = 12, UserProfileId = 21 }
            };
        }

        private IEnumerable<PostModel> GetTestPostModels ()
        {
            return new List<PostModel>()
            {
                new PostModel { Id = 1, Content = "First Post", PostDate = DateTime.Now, ThreadId = 10, UserProfileId = 20 },
                new PostModel { Id = 2, Content = "Second Post", PostDate = DateTime.Now, ThreadId = 10, UserProfileId = 20 },
                new PostModel { Id = 3, Content = "Third Post", PostDate = DateTime.Now, ThreadId = 11, UserProfileId = 21 },
                new PostModel { Id = 4, Content = "Fourth Post", PostDate = DateTime.Now, ThreadId = 12, UserProfileId = 21 },
                new PostModel { Id = 5, Content = "Fifth Post", PostDate = DateTime.Now, ThreadId = 12, UserProfileId = 21 }
            };
        }

        private IEnumerable<UserProfile> GetTestUserProfiles ()
        {
            return new List<UserProfile>()
            {
                new UserProfile {Id = 20, ApplicationUserId = "", Rating = 100 },
                new UserProfile {Id = 21, ApplicationUserId = "", Rating = 100 }
            };
        }

        [Test]
        public async Task PostService_GetAllAsync_ReturnsAllPosts()
        {
            var expected = GetTestPostModels().ToList();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Posts.GetAllAsync())
                .ReturnsAsync(GetTestPostEntities());
            var service = new PostService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var posts = await service.GetAllAsync();
            var actual = posts.ToList();

            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Content, actual[i].Content);
                Assert.AreEqual(expected[i].ThreadId, actual[i].ThreadId);
                Assert.AreEqual(expected[i].UserProfileId, actual[i].UserProfileId);
            }
        }

        [Test]
        public async Task PostService_GetAllById_ReturnsSinglePostModel()
        {
            var expected = GetTestPostModels().First();
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Posts.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestPostEntities().First);

            var service = new PostService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var actual = await service.GetByIdAsync(1);

            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Content, actual.Content);
            Assert.AreEqual(expected.ThreadId, actual.ThreadId);
            Assert.AreEqual(expected.UserProfileId, actual.UserProfileId);
        }

        [TestCase(10)]
        [TestCase(11)]
        [TestCase(12)]
        public async Task PostService_GetPostsByThreadId_ReturnsPostsByThread(int threadId)
        {
            var expected = GetTestPostModels().Where( p => p.ThreadId == threadId).ToList();

            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork
                .Setup(m => m.Posts.GetAllAsync())
                .ReturnsAsync(GetTestPostEntities());

            var service = new PostService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());

            var posts = await service.GetPostsByThreadId(threadId);
            var actual = posts.ToList();

            for (int i = 0; i < actual.Count(); i++)
            {
                Assert.AreEqual(expected[i].Id, actual[i].Id);
                Assert.AreEqual(expected[i].Content, actual[i].Content);
                Assert.AreEqual(expected[i].ThreadId, actual[i].ThreadId);
                Assert.AreEqual(expected[i].UserProfileId, actual[i].UserProfileId);
            }
        }

        [Test]
        public async Task PostService_RemoveAsync_RemoveValueFromDatabase()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Posts.Remove(It.IsAny<Post>()));
            mockUnitOfWork.Setup(x => x.UserProfiles.GetWithIncludeAsync()).ReturnsAsync(GetTestUserProfiles());
            mockUnitOfWork
                .Setup(m => m.Posts.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(GetTestPostEntities().First);

            var service = new PostService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var post = GetTestPostModels().First();

            await service.RemoveAsync(post);

            mockUnitOfWork.Verify(x => x.Posts.Remove(It.Is<Post>(p => p.Id == post.Id)), Times.Once);
            mockUnitOfWork.Verify(x => x.UserProfiles.Update(It.Is<UserProfile>(p => p.Id == post.UserProfileId)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Exactly(2));
        }

        [Test]
        public async Task PostService_UpdateAsync_UpdateEntityInDatabase()
        {
            var mockUnitOfWork = new Mock<IUnitOfWork>();
            mockUnitOfWork.Setup(x => x.Posts.Update(It.IsAny<Post>()));
            var service = new PostService(mockUnitOfWork.Object, UnitTestsHelper.CreateMapperProfile());
            var post = new PostModel { Id = 4, Content = "Fourth Updated Post", PostDate = DateTime.Now, ThreadId = 11, UserProfileId = 21 };

            await service.UpdateAsync(post);

            mockUnitOfWork.Verify(x => x.Posts.Update(It.Is<Post>(p => p.Id == post.Id && p.Content == post.Content)), Times.Once);
            mockUnitOfWork.Verify(x => x.SaveChangesAsync(), Times.Once);
        }
    }
}
