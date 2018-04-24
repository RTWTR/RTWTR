using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.CollectionService.Tests
{
    [TestClass]
    public class AddTweetToCollection_Should
    {
        private Mock<ISaver> saverStub;
        private Mock<IMappingProvider> mapperStub;
        private Mock<IRepository<Collection>> collectionsRepositoryStub;
        private Mock<IRepository<Tweet>> tweetsRepositoryStub;
        private Mock<IRepository<CollectionTweet>> collectionTweetsRepositoryStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.saverStub = new Mock<ISaver>();
            this.mapperStub = new Mock<IMappingProvider>();
            this.collectionsRepositoryStub = new Mock<IRepository<Collection>>();
            this.tweetsRepositoryStub = new Mock<IRepository<Tweet>>();
            this.collectionTweetsRepositoryStub = new Mock<IRepository<CollectionTweet>>();
        }

        [TestMethod]
        public void Throw_InvalidCollectionIdException_When_CollectionIdIsNull()
        {
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidCollectionIdException>(() =>
            {
                collectionServie.AddTweetToCollection(null, "tweetId");
            });
        }

        [TestMethod]
        public void Throw_InvalidUserIdException_When_CollectionIdIsEmpty()
        {
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidUserIdException>(() =>
            {
                collectionServie.AddTweetToCollection(" ", "tweetId");
            });
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsNull()
        {
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidTweetIdException>(() =>
            {
                collectionServie.AddTweetToCollection("collectionId", null);
            });
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsEmpty()
        {
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidTweetIdException>(() =>
            {
                collectionServie.AddTweetToCollection("collectionId", " ");
            });
        }

        [TestMethod]
        public void Throw_NullCollectionException_When_CollectionIsNotFound()
        {
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            Assert.ThrowsException<NullCollectionException>(() =>
            {
                collectionServie.AddTweetToCollection("collectionId", "tweetId");
            });
        }

        [TestMethod]
        public void Throw_NullTweetException_When_TweetIsNotFound()
        {
            this.collectionsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                );

            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            Assert.ThrowsException<NullTweetException>(() =>
            {
                collectionServie.AddTweetToCollection("collectionId", "tweetId");
            });
        }

        [TestMethod]
        public void Call_CollectionRepository_All_Once()
        {
            this.collectionsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                )
                .Verifiable();
            
            this.tweetsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "tweetId" }
                    }.AsQueryable()
                );

            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            collectionServie.AddTweetToCollection("collectionId", "tweetId");

            this.collectionsRepositoryStub.Verify(
                x => x.All, 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_TweetRepository_All_Once()
        {
            this.collectionsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                );
            
            this.tweetsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "tweetId" }
                    }.AsQueryable()
                )
                .Verifiable();

            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            collectionServie.AddTweetToCollection("collectionId", "tweetId");

            this.tweetsRepositoryStub.Verify(
                x => x.All, 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_CollectionTweetRepository_Add_Once()
        {
            this.collectionTweetsRepositoryStub
                .Setup(x => x.Add(It.IsAny<CollectionTweet>()))
                .Verifiable();

            this.collectionsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                );
            
            this.tweetsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "tweetId" }
                    }.AsQueryable()
                );

            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            collectionServie.AddTweetToCollection("collectionId", "tweetId");

            this.collectionTweetsRepositoryStub.Verify(
                x => x.Add(It.IsAny<CollectionTweet>()), 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_Saver_SaveChanges_Once()
        {
            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1)
                .Verifiable();

            this.collectionsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                );
            
            this.tweetsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "tweetId" }
                    }.AsQueryable()
                );

            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            collectionServie.AddTweetToCollection("collectionId", "tweetId");

            this.saverStub.Verify(
                x => x.SaveChanges(), 
                Times.Once
            );
        }

        [TestMethod]
        public void Return_One_When_SuccessfullyAddedUserToFavourites()
        {
            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            this.collectionsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                );
            
            this.tweetsRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Tweet>()
                    {
                        new Tweet() { Id = "tweetId" }
                    }.AsQueryable()
                );

            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            Assert.AreEqual(
                1,
                collectionServie.AddTweetToCollection("collectionId", "tweetId")
            );
        }
    }
}