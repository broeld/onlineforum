using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.ViewModels;

namespace OnlineForum.IntegrationTests.EndpointTests
{
    [TestFixture]
    public class PostsTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUri = "api/posts/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();

        }

        [Test]
        public async Task PostsController_Get_ReturnsPosts()
        {
            var httpResponse = await _client.GetAsync(RequestUri);
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<IEnumerable<PostDetailModel>>(stringResponse);

            Assert.AreEqual(6, posts.Count());
        }

        [Test]
        public async Task PostsController_GetById_ReturnsPost()
        {
            var httpResponse = await _client.GetAsync(RequestUri + 1);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var post = JsonConvert.DeserializeObject<PostDetailModel>(stringResponse);

            Assert.AreEqual(1, post.Id);
            Assert.AreEqual("First reply to thread", post.Content);
            Assert.AreEqual(1, post.UserProfileId);
        }

        [Test]
        public async Task PostsController_GetPostsByThreadId_ReturnsThreadPosts()
        {
            var httpResponse = await _client.GetAsync("/api/threads/151/posts");

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var posts = JsonConvert.DeserializeObject<IEnumerable<PostDetailModel>>(stringResponse);

            Assert.AreEqual(2, posts.Count());
        }
    }
}
