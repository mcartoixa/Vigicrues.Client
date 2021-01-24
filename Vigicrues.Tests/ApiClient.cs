using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Moq;
using Moq.Protected;
using Xunit;

namespace Vigicrues
{

    public class ApiClientTests
    {

        private Mock<HttpMessageHandler> _CreateFakeHttpMessageHandler(HttpStatusCode code, string response)
        {
            var ret = new Mock<HttpMessageHandler>(MockBehavior.Strict);
            ret
               .Protected()
               .Setup<Task<HttpResponseMessage>>(
                  "SendAsync",
                  ItExpr.IsAny<HttpRequestMessage>(),
                  ItExpr.IsAny<CancellationToken>()
               )
               .ReturnsAsync(new HttpResponseMessage() {
                   StatusCode = code,
                   Content = new StringContent(response),
               })
               .Verifiable();
            return ret;
        }

        [Fact]
        public async void GetTerritoriesAsync_ShouldReturnAListOfTerritories()
        {
            // Arrange
            var fakeHttpMessageHandler = _CreateFakeHttpMessageHandler(HttpStatusCode.OK, Resources.HttpResponses.TerEntVigiCru);
            var client = new ApiClient(fakeHttpMessageHandler.Object, true);

            // Act
            var result = await client.GetTerritoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            fakeHttpMessageHandler.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get && req.RequestUri == new Uri("https://www.vigicrues.gouv.fr/services/1/TerEntVigiCru.jsonld/")
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async void GetTerritoryAsync_ShouldReturnTerritory()
        {
            // Arrange
            var fakeHttpMessageHandler = _CreateFakeHttpMessageHandler(HttpStatusCode.OK, Resources.HttpResponses.TerEntVigiCru_CdEntVigiCru25);
            var client = new ApiClient(fakeHttpMessageHandler.Object, true);
            var input = new TerritoryEntity("25");

            // Act
            var result = await client.GetTerritoryAsync(input);

            // Assert
            Assert.NotNull(result);

            fakeHttpMessageHandler.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get && req.RequestUri == new Uri("https://www.vigicrues.gouv.fr/services/1/TerEntVigiCru.jsonld/?CdEntVigiCru=25&TypEntVigiCru=5")
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }
    }
}
