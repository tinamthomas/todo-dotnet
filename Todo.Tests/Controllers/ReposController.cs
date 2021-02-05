using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Moq;
using Moq.Protected;
using NUnit.Framework;
using TodoApi.Controllers;

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
            string json = "[{\"name\":\"Person1\"}]";

            mockHttpMessageHandler.Protected()  
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())  
                .ReturnsAsync(new HttpResponseMessage  
                {  
                    StatusCode = HttpStatusCode.OK,  
                    Content = new StringContent(json),
                });  
  
            var client = new HttpClient(mockHttpMessageHandler.Object);
            httpClientFactory.Setup(_ => _.CreateClient(It.IsAny<string>())).Returns(client);  
            
            var controller = new ReposController(httpClientFactory.Object);

            var result = await controller.GetRepos();
            Assert.NotNull(result);
        }
    }
}