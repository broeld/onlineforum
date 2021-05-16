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
    public class NotificationTests
    {
        private HttpClient _client;
        private CustomWebApplicationFactory _factory;
        private const string RequestUri = "api/notification/";

        [SetUp]
        public void Setup()
        {
            _factory = new CustomWebApplicationFactory();
            _client = _factory.CreateClient();

        }

        [Test]
        public async Task NotificationController_GetAll_ReturnsNotifications()
        {
            var httpResponse = await _client.GetAsync(RequestUri);

            httpResponse.EnsureSuccessStatusCode();
            var stringResponse = await httpResponse.Content.ReadAsStringAsync();
            var notifications = JsonConvert.DeserializeObject<IEnumerable<NotificationViewModel>>(stringResponse);

            Assert.AreEqual(2, notifications.Count());
        }

        public async Task NotificationController_DeleteById_DeleteEntity()
        {
            var httpResponse = await _client.DeleteAsync(RequestUri + 52);
            httpResponse.EnsureSuccessStatusCode();

            Assert.AreEqual(200, httpResponse.StatusCode);
        }
    }
}
