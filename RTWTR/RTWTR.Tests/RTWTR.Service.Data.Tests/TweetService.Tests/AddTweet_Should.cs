using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TweetService.Tests
{
    // TODO: Finish tests
    [TestClass]
    public class AddTweet_Should
    {
        private Mock<ISaver> saverStub;
        private Mock<IMappingProvider> mapperStub;
        private Mock<IRepository<Tweet>> tweetRepositoryStub;
        private Mock<IRepository<UserTweet>> UserTweetStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.saverStub = new Mock<ISaver>();
            this.mapperStub = new Mock<IMappingProvider>();
            this.tweetRepositoryStub = new Mock<IRepository<Tweet>>();
            this.UserTweetStub = new Mock<IRepository<UserTweet>>();
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsNull()
        {
            // Arrange
            var userDtoStub = new UserDTO();

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.UserTweetStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidTweetIdException>(() =>
                tweetService.AddTweetToFavourites(null, userDtoStub)
            );
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsWhitespace()
        {
            // Arrange
            var userDtoStub = new UserDTO();

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.UserTweetStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidTweetIdException>(() =>
                tweetService.AddTweetToFavourites(" ", userDtoStub)
            );
        }

        [TestMethod]
        public void ReturnOne_When_TweetSuccessfullySaved()
        {
            // Arrange
            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.UserTweetStub.Object
            );

            var tweetToSave = new Tweet() { Id = "tweetId", Text = "text" };

            // Act & Assert
            Assert.AreEqual(1, tweetService.AddTweetToFavourites("tweetId", null));
        }
    }
}