namespace ScheduleGenerator.Tests
{
    using Moq;
    using System.Net.Http;
    using Xunit;

    public class HttpServiceUnitTests
    {
        [Fact]
        public void GetRecipeData_ReturnsOkResponseOnValidUrl()
        {
            // Arrange
            var handlerMock = new Mock<HttpMessageHandler>();
            var magicHttpClient = new HttpClient(handlerMock.Object);

            // Act

            // Assert
        }


        [Fact]
        public void Test1()
        {
            // Arrange

            // Act

            // Assert
        }
    }
}
