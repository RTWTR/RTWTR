using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TweetService.Tests
{
    [TestClass]
    public class RemoveTweetFromFavourites_Should
    {
        private Mock<ISaver> saverStub;
        private Mock<IMappingProvider> mapperStub;
        private Mock<IRepository<Tweet>> tweetRepositoryStub;
        private Mock<IRepository<UserTweet>> userTweetsStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.saverStub = new Mock<ISaver>();
            this.mapperStub = new Mock<IMappingProvider>();
            this.tweetRepositoryStub = new Mock<IRepository<Tweet>>();
            this.userTweetsStub = new Mock<IRepository<UserTweet>>();
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsNull()
        {
            // Arrange
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidTweetIdException>(() =>
            {
                tweetService.RemoveTweetFromFavourites(null, "userId");
            });
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsWhitespace()
        {
            // Arrange
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidTweetIdException>(() =>
            {
                tweetService.RemoveTweetFromFavourites(" ", "userId");
            });
        }

        [TestMethod]
        public void Throw_InvalidUserIdException_When_UserIdIsNull()
        {
            // Arrange
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidUserIdException>(() =>
            {
                tweetService.RemoveTweetFromFavourites("tweetId", null);
            });
        }

        [TestMethod]
        public void Throw_InvalidUserIdException_When_UserIdIsWhitespace()
        {
            // Arrange
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidUserIdException>(() =>
            {
                tweetService.RemoveTweetFromFavourites("tweetId", " ");
            });
        }

        [TestMethod]
        public void Throw_NullTweetException_When_TweetIsNotFound()
        {
            // Arrange
            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    { }.AsQueryable());

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            Assert.ThrowsException<NullTweetException>(() =>
            {
                tweetService.RemoveTweetFromFavourites("tweetId", "userId");
            });

            this.tweetRepositoryStub.Verify(
                x => x.All,
                Times.Once
            );
        }

        [TestMethod]
        public void Throw_ArgumentException_When_TweetIsNotAFavourite()
        {
            // Arrange
            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet { Id = "tweetId", TwitterId = "tweetId" }
                    }.AsQueryable());

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            Assert.ThrowsException<ArgumentException>(() =>
            {
                tweetService.RemoveTweetFromFavourites("tweetId", "userId");
            });
        }

        [TestMethod]
        public void Return_One_When_TweetSuccessfullyReRemoved()
        {
            // Arrange
            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet { Id = "tweetId", TwitterId = "tweetId" }
                    }.AsQueryable());

            this.userTweetsStub
                .Setup(x => x.All)
                .Returns(
                    new List<UserTweet>()
                    {
                        new UserTweet { TweetId = "tweetId", UserId = "userId" }
                    }.AsQueryable()
                );

            this.userTweetsStub
                .Setup(x => x.Delete(It.IsAny<UserTweet>()))
                .Verifiable();

            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            // Act
            var actual = tweetService.RemoveTweetFromFavourites(
                "tweetId",
                "userId"
            );

            // Assert
            Assert.AreEqual(
                1,
                actual
            );

            this.userTweetsStub.Verify(
                x => x.Delete(It.IsAny<UserTweet>()),
                Times.Once
            );
        }
    }
}