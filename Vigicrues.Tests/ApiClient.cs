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
        public async void GetSectionsAsync_ShouldReturnAListOfSections()
        {
            // Arrange
            var fakeHttpMessageHandler = _CreateFakeHttpMessageHandler(HttpStatusCode.OK, Resources.HttpResponses.TronEntVigiCru);
            var client = new ApiClient(fakeHttpMessageHandler.Object, true);

            // Act
            var result = await client.GetSectionsAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);

            fakeHttpMessageHandler.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get && req.RequestUri == new Uri("https://www.vigicrues.gouv.fr/services/1/TronEntVigiCru.jsonld/")
               ),
               ItExpr.IsAny<CancellationToken>()
            );
        }

        [Fact]
        public async void GetSectionAsync_ShouldReturnSection()
        {
            // Arrange
            var fakeHttpMessageHandler = _CreateFakeHttpMessageHandler(HttpStatusCode.OK, Resources.HttpResponses.TronEntVigiCru_CdEntVigiCruAP1);
            var client = new ApiClient(fakeHttpMessageHandler.Object, true);
            var input = new SectionEntity("AP1");

            // Act
            var result = await client.GetSectionAsync(input);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("AP1", result.Reference);
            Assert.Equal(3, result.Children.Length);

            fakeHttpMessageHandler.Protected().Verify(
               "SendAsync",
               Times.Exactly(1),
               ItExpr.Is<HttpRequestMessage>(req =>
                  req.Method == HttpMethod.Get && req.RequestUri == new Uri("https://www.vigicrues.gouv.fr/services/1/TronEntVigiCru.jsonld/?CdEntVigiCru=AP1&TypEntVigiCru=8")
               ),
               ItExpr.IsAny<CancellationToken>()
            );
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
            Assert.Equal("25", result.Reference);

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
