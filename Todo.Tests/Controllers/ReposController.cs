using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using TodoApi.Controllers;
using TodoApi.Models;

namespace Todo.Tests
{
    public class ReposControllerTests
    {
        [SetUp]
        public void Setup()
        {
           
        }

        [Test]
        public async Task Test1()
        {
            var httpClientFactory = new Mock<IHttpClientFactory>();  
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            Repo repo1 = new Repo("Repo1");
            Repo repo2 = new Repo("Repo2");
            Repo[] repos = new Repo[] {repo1, repo2};
            string reposJson = JsonSerializer.Serialize(repos);
            mockHttpMessageHandler.Protected()  
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())  
                .ReturnsAsync(new HttpResponseMessage  
                {  
                    StatusCode = HttpStatusCode.OK,  
                    Content = new StringContent(reposJson),
                });  
  
            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);
            var controller = new ReposController(httpClientFactory.Object);

            var result = await controller.GetRepos();
            
            Assert.AreEqual(reposJson, JsonSerializer.Serialize(result.Value));
        }
    }
}