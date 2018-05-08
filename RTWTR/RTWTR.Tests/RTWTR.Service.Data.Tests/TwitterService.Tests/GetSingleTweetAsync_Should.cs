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
    public class GetSingleTweetAsync_Should
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
                .Setup(x => x.DeserializeObject<TweetDto>(It.IsAny<string>()))
                .Returns(new TweetDto());

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var TweetDto = await twitterService.GetSingleTweetAsync("screenName");

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
                .Setup(x => x.DeserializeObject<TweetDto>(It.IsAny<string>()))
                .Returns(new TweetDto())
                .Verifiable();

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var TweetDto = await twitterService.GetSingleTweetAsync("screenName");

            // Assert
            this.jsonProviderStub.Verify(
                x => x.DeserializeObject<TweetDto>(It.IsAny<string>()),
                Times.Once
            );
        }

        [TestMethod]
        public async Task Returns_EmptyTweet_When_ResponseIsWhitespace()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync("");

            this.jsonProviderStub
                .Setup(x => x.DeserializeObject<TweetDto>(It.IsAny<string>()))
                .Returns(new TweetDto())
                .Verifiable();

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var TweetDto = await twitterService.GetSingleTweetAsync("screenName");

            // Assert
            Assert.IsNotNull(TweetDto);
            Assert.IsNull(TweetDto.Id);
            Assert.IsNull(TweetDto.Text);
            Assert.IsNull(TweetDto.TwitterUser);
            Assert.IsNull(TweetDto.CreatedAt);
        }

        [TestMethod]
        public async Task Returns_CorrectTweet_When_ResponseIsCorrect()
        {
            // Arrange 
            this.apiProviderStub
                .Setup(x => x.GetJSON(It.IsAny<string>()))
                .ReturnsAsync("correctJSON");

            this.jsonProviderStub
                .Setup(x => x.DeserializeObject<TweetDto>("correctJSON"))
                .Returns(
                    new TweetDto() 
                    { 
                        Id = "ID",
                        Text = "Tweet Text",
                        CreatedAt = "createdAt",
                        TwitterUser = new TwitterUserDto
                        {
                            Id = "userId",
                            Name = "userName",
                            ScreenName = "userScreenName"
                        }
                    })
                .Verifiable();

            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Act
            var TweetDto = await twitterService.GetSingleTweetAsync("screenName");

            // Assert
            Assert.IsNotNull(TweetDto);
            Assert.IsNotNull(TweetDto.TwitterUser);
            Assert.AreEqual("ID", TweetDto.Id);
            Assert.AreEqual("Tweet Text", TweetDto.Text);
            Assert.AreEqual("createdAt", TweetDto.CreatedAt);
            Assert.AreEqual("userId", TweetDto.TwitterUser.Id);
            Assert.AreEqual("userName", TweetDto.TwitterUser.Name);
            Assert.AreEqual("userScreenName", TweetDto.TwitterUser.ScreenName);
        }
    }
}
