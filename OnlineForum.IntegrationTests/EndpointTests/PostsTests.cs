using NUnit.Framework;
using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Web.ViewModels;

namespace OnlineForum.IntegrationTests.ControllerTests
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
        }
    }
}
