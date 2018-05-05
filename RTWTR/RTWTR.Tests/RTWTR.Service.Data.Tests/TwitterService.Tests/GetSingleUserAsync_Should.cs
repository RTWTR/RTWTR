using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.DTO;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TwitterService.Tests
{
    [TestClass]
    public class GetSingleUserAsync_Should
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

            this.jsonProviderStub
                .Setup(x => x.DeserializeObject<TwitterUserDto>(It.IsAny<string>()))
                .Returns(new TwitterUserDto());

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var userDto = await twitterService.GetSingleUserAsync("screenName");

            // Assert
            this.apiProviderStub.Verify(
                x => x.GetJSON(It.IsAny<string>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task Call_JsonProvider_Deserialize_Once()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync("correctJSON");

            this.jsonProviderStub
                .Setup(x => x.DeserializeObject<TwitterUserDto>(It.IsAny<string>()))
                .Returns(new TwitterUserDto())
                .Verifiable();

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var userDto = await twitterService.GetSingleUserAsync("screenName");

            // Assert
            this.jsonProviderStub.Verify(
                x => x.DeserializeObject<TwitterUserDto>(It.IsAny<string>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task Returns_EmptyUser_When_ResponseIsWhitespace()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync("");

            this.jsonProviderStub
                .Setup(x => x.DeserializeObject<TwitterUserDto>(It.IsAny<string>()))
                .Returns(new TwitterUserDto())
                .Verifiable();

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var userDto = await twitterService.GetSingleUserAsync("screenName");

            // Assert
            Assert.IsNotNull(userDto);
            Assert.IsNull(userDto.Id);
            Assert.IsNull(userDto.Name);
            Assert.IsNull(userDto.Description);
            Assert.IsNull(userDto.TwitterId);
            Assert.IsNull(userDto.ProfileImageUrl);
        }

        [TestMethod]
        public async Task Returns_CorrectUser_When_ResponseIsCorrect()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync("correctJSON");

            this.jsonProviderStub
                .Setup(x => x.DeserializeObject<TwitterUserDto>("correctJSON"))
                .Returns(
                    new TwitterUserDto() 
                    { 
                        Id = "ID",
                        Name = "Jason",
                        Description = "CorrectDTO"
                    })
                .Verifiable();

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var userDto = await twitterService.GetSingleUserAsync("screenName");

            // Assert
            Assert.IsNotNull(userDto);
            Assert.AreEqual("ID", userDto.Id);
            Assert.AreEqual("Jason", userDto.Name);
            Assert.AreEqual("CorrectDTO", userDto.Description);
        }
    }
}
