using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.CollectionService.Tests
{
    [TestClass]
    public class AddTweetToCollection_Should
    {
        private Mock<ISaver> saverStub;
        private Mock<IMappingProvider> mapperStub;
        private Mock<IRepository<Tweet>> tweetRepositoryStub;
        private Mock<IRepository<Collection>> collectionRepositoryStub;
        private Mock<IRepository<CollectionTweet>> collectionTweetsRepositoryStub;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            this.saverStub = new Mock<ISaver>();
            this.mapperStub = new Mock<IMappingProvider>();
            this.tweetRepositoryStub = new Mock<IRepository<Tweet>>();
            this.collectionRepositoryStub = new Mock<IRepository<Collection>>();
            this.collectionTweetsRepositoryStub = new Mock<IRepository<CollectionTweet>>();
        }

        [TestMethod]
        public void ReturnMinusOne_When_InvokedWithNullCollectionId()
        {
            //Arrange
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            //Action & Assert
            Assert.AreEqual(-1, collectionService.AddTweetToCollection(null, "tweetId"));
        }

        [TestMethod]
        public void ReturnMinusOne_When_InvokedWithNullTweetId()
        {
            //Arrange
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            //Action & Assert
            Assert.AreEqual(-1, collectionService.AddTweetToCollection("collectionId", null));
        }

        [TestMethod]
        public void ReturnMinusOne_When_TweetIsNotFound()
        {
            //Arrange
            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "notThisOne" }
                    }.AsQueryable()
                );

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.AreEqual(-1, collectionService.AddTweetToCollection("collectionId", "thisOne"));
        }

        [TestMethod]
        public void ReturnMinusOne_When_CollectionIsNotFound()
        {
            //Arrange
            this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "tweetId" }
                    }.AsQueryable()
                );

            this.collectionRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "notThisOne" }
                    }.AsQueryable()
                );

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.AreEqual(-1, collectionService.AddTweetToCollection("thisOne", "tweetId"));
        }

        [TestMethod]
        public void ReturnOne_When_TweetSuccessfullyAddedToCollection()
        {
            //Arrange
             this.tweetRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "tweetId" }
                    }.AsQueryable()
                );

            this.collectionRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                );

            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            Assert.AreEqual(1, collectionService.AddTweetToCollection("collectionId", "tweetId"));
        }
    }
}