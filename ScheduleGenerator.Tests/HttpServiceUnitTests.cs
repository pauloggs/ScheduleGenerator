namespace ScheduleGenerator.Tests
{
    using Moq;
    using Moq.Protected;
    using ScheduleGenerator.Services;
    using System;
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using Xunit;

    public class HttpServiceUnitTests
    {
        [Fact]
        public async void GetRecipeData_ReturnsDataWithValidHttpClientSetup()
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent($"Some message")
                })
                .Verifiable();

            var httpClient = new HttpClient(httpMessageHandlerMock.Object) { BaseAddress = new System.Uri("http://localhost:8080/") };
            var sut = new HttpService(httpClient);

            // Act
            var result = await sut.GetRecipeData();

            // Assert
            Assert.Equal("Some message", result);
        }


        [Fact]
        public async Task GetRecipeData_ReturnsErrorTextWithInvalidHttpClientSetup()
        {
            // Arrange
            var httpMessageHandlerMock = new Mock<HttpMessageHandler>(MockBehavior.Loose);
            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.InternalServerError,
                    Content = new StringContent($"Some error")
                })
                .Verifiable();

            httpMessageHandlerMock
                .Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ThrowsAsync(new Exception());

            var httpClient = new HttpClient(httpMessageHandlerMock.Object) { BaseAddress = new System.Uri("http://localhost:8080/") };
            var sut = new HttpService(httpClient);

            // Assert
            await Assert.ThrowsAsync<Exception>(() => sut.GetRecipeData());
        }
    }
}
