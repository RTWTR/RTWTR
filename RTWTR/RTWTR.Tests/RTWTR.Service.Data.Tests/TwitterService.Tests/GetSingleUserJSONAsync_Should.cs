using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TwitterService.Tests
{
    [TestClass]
    public class GetSingleUserJSONAsync_Should
    {
        private Mock<IApiProvider> apiProviderStub;
        private Mock<IJsonProvider> jsonProviderStub;
        private Mock<IMappingProvider> mappingProviderStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.apiProviderStub = new Mock<IApiProvider>();
            this.jsonProviderStub = new Mock<IJsonProvider>();
            this.mappingProviderStub = new Mock<IMappingProvider>();
        }

        [TestMethod]
        public async Task Call_ApiProvider_GetJSON_Once()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync("correctJSON")
                .Verifiable();

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var result = await twitterService.GetSingleUserJSONAsync("screenName");

            // Assert
            this.apiProviderStub.Verify(
                x => x.GetJSON(It.IsAny<string>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task Return_CorrectJSON_When_ScreenNameIsValid()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync("correctJSON");

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            var result = await twitterService.GetSingleUserJSONAsync("screenName");

            Assert.AreEqual(
                "correctJSON",
                result
            );
        }

        [TestMethod]
        public async Task Return_EmptyString_When_ResponseIsWhitespace()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync(" ");

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var result = await twitterService.GetSingleUserJSONAsync("screenName");

            // Assert
            Assert.AreEqual(
                string.Empty,
                result
            );
        }

        [TestMethod]
        public async Task Return_EmptyString_When_ResponseIsNull()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync((string)null);

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var result = await twitterService.GetSingleUserJSONAsync("screenName");

            // Assert
            Assert.AreEqual(
                string.Empty,
                result
            );
        }
    }
}
