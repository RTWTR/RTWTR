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
    // TODO: Finish tests
    [TestClass]
    public class AddTweet_Should
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
            var userDtoStub = new UserDTO();

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
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
                this.userTweetsStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidTweetIdException>(() =>
                tweetService.AddTweetToFavourites(" ", userDtoStub)
            );
        }

        [TestMethod]
        public void Throw_NullUserException_When_UserDtoIsNull()
        {
            // Arrange
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<NullUserException>(() =>
                tweetService.AddTweetToFavourites("tweetId", null)
            );
        }

        [TestMethod]
        public void Throw_NullTweetException_When_TweetIsNotFound()
        {
            // Arrange
            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                new List<Tweet>()
                {
                }.AsQueryable());

            var userDtoStub = new UserDTO();

            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            Assert.ThrowsException<NullTweetException>(() =>
            {
                tweetService.AddTweetToFavourites("tweetId", userDtoStub);
            });

            this.tweetRepositoryStub.Verify(
                x => x.All,
                Times.Once
            );
        }

        [TestMethod]
        public void Return_One_When_TweetSuccessfullyAdded()
        {
            // Arrange
            this.userTweetsStub
                .Setup(x => x.Add(It.IsAny<UserTweet>()))
                .Verifiable();

            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                new List<Tweet>()
                {
                    new Tweet { TwitterId = "tweetId" }
                }.AsQueryable());

            this.mapperStub
                .Setup(x => x.MapTo<User>(It.IsAny<UserDTO>()))
                .Returns(new User { Id = "userId" });

            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            var userDtoStub = new UserDTO();
            
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            var actual = tweetService.AddTweetToFavourites(
                "tweetId",
                userDtoStub
            );

            Assert.AreEqual(
                1,
                actual
            );

            this.userTweetsStub.Verify(
                x => x.Add(It.IsAny<UserTweet>()),
                Times.Once
            );
        }

        [TestMethod]
        public void Return_One_When_TweetSuccessfullyReAdded()
        {
            // Arrange
            this.userTweetsStub
                .Setup(x => x.AllAndDeleted)
                .Returns(
                    new List<UserTweet>()
                    {
                        new UserTweet { TweetId = "tweetId", UserId = "userId", IsDeleted = true }
                    }.AsQueryable()
                )
                .Verifiable();

            this.userTweetsStub
                .Setup(x => x.Update(It.IsAny<UserTweet>()))
                .Verifiable();

            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                new List<Tweet>()
                {
                    new Tweet { Id = "tweetId", TwitterId = "tweetId" }
                }.AsQueryable());

            this.tweetRepositoryStub
                .Setup(x => x.AllAndDeleted)
                .Returns(
                new List<Tweet>()
                {
                    new Tweet { Id = "tweetId", TwitterId = "tweetId" }
                }.AsQueryable());

            this.mapperStub
                .Setup(x => x.MapTo<User>(It.IsAny<UserDTO>()))
                .Returns(new User { Id = "userId" });

            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            var userDtoStub = new UserDTO();
            
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            var actual = tweetService.AddTweetToFavourites(
                "tweetId",
                userDtoStub
            );

            Assert.AreEqual(
                1,
                actual
            );

            this.userTweetsStub.Verify(
                x => x.Update(It.IsAny<UserTweet>()),
                Times.Once
            );
        }

        [TestMethod]
        public void Throw_ArgumentException_When_TweetAlreadyFavourite()
        {
            // Arrange
            this.userTweetsStub
                .Setup(x => x.AllAndDeleted)
                .Returns(
                    new List<UserTweet>()
                    {
                        new UserTweet { TweetId = "tweetId", UserId = "userId", IsDeleted = false }
                    }.AsQueryable()
                )
                .Verifiable();

            this.userTweetsStub
                .Setup(x => x.Update(It.IsAny<UserTweet>()))
                .Verifiable();

            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                new List<Tweet>()
                {
                    new Tweet { Id = "tweetId", TwitterId = "tweetId", IsDeleted = false }
                }.AsQueryable());

            this.tweetRepositoryStub
                .Setup(x => x.AllAndDeleted)
                .Returns(
                new List<Tweet>()
                {
                    new Tweet { Id = "tweetId", TwitterId = "tweetId" }
                }.AsQueryable());

            this.mapperStub
                .Setup(x => x.MapTo<User>(It.IsAny<UserDTO>()))
                .Returns(new User { Id = "userId" });

            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            var userDtoStub = new UserDTO();
            
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object,
                this.userTweetsStub.Object
            );

            Assert.ThrowsException<ArgumentException>(() => {
                tweetService.AddTweetToFavourites("tweetId", userDtoStub);
            });
        }
    }
}