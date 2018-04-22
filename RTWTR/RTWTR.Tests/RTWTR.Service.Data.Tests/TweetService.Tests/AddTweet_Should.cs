using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TweetService.Tests
{
    [TestClass]
    public class AddTweet_Should
    {
        private Mock<ISaver> saverStub;
        private Mock<IMappingProvider> mapperStub;
        private Mock<IRepository<Tweet>> tweetRepositoryStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.saverStub = new Mock<ISaver>();
            this.mapperStub = new Mock<IMappingProvider>();
            this.tweetRepositoryStub = new Mock<IRepository<Tweet>>();
        }

        [TestMethod]
        public void ReturnMinusOne_When_TweetToSaveIsNull()
        {
            // Arrange
            var tweetService = new global::RTWTR.Service.Data.TweetService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.tweetRepositoryStub.Object
            );

            // Act & Assert
            Assert.AreEqual(-1, tweetService.AddTweet(null));
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
                this.tweetRepositoryStub.Object
            );

            var tweetToSave = new Tweet() { Id = "tweetId", Text = "text" };

            // Act & Assert
            Assert.AreEqual(1, tweetService.AddTweet(tweetToSave));
        }
    }
}