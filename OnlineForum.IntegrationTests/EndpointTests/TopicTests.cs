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
    public class TopicTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUri = "api/topics/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();

        }

        [Test]
        public async Task TopicController_Get_ReturnsTopics()
        {
            var httpResponse = await _client.GetAsync(RequestUri);
            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var topics = JsonConvert.DeserializeObject<IEnumerable<TopicDetailModel>>(stringResponse);

            Assert.AreEqual(3, topics.Count());
        }

        [Test]
        public async Task TopicsController_GetById_ReturnsTopic()
        {
            var httpResponse = await _client.GetAsync(RequestUri + 1001);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var topic = JsonConvert.DeserializeObject<TopicDetailModel>(stringResponse);

            Assert.AreEqual(1001, topic.Id);
            Assert.AreEqual("Python", topic.Title);
            Assert.AreEqual("Python is the best programming language ever", topic.Description);
        }
    }
}
