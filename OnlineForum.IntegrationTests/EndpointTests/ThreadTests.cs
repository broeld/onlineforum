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
    public class ThreadTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUri = "api/threads/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();

        }

        [Test]
        public async Task ThreadController_Get_ReturnsThreads()
        {
            var httpResponse = await _client.GetAsync(RequestUri);
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var threads = JsonConvert.DeserializeObject<IEnumerable<ThreadDisplayViewModel>>(stringResponse);

            Assert.AreEqual(4, threads.Count());
        }

        [Test]
        public async Task PostsController_GetById_ReturnsPost()
        {
            var httpResponse = await _client.GetAsync(RequestUri + 151);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var thread = JsonConvert.DeserializeObject<ThreadDisplayViewModel>(stringResponse);

            Assert.AreEqual(151, thread.Id);
            Assert.AreEqual("Test thread 1", thread.Title);
            Assert.AreEqual("Some content", thread.Content);
        }
        
        [Test]
        public async Task PostsController_GetThreadsByTopicId_ReturnsThreads()
        {
            var httpResponse = await _client.GetAsync("api/topics/1001/threads");
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var threads = JsonConvert.DeserializeObject<IEnumerable<ThreadDisplayViewModel>>(stringResponse);

            Assert.AreEqual(2, threads.Count());
        }
    }
}
