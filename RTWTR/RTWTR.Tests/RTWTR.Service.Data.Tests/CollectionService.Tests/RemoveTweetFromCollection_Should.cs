﻿using System;
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
    public class RemoveTweetFromCollection_Should
    {
        private Mock<ISaver> saverStub;
        private Mock<IMappingProvider> mapperStub;
        private Mock<IRepository<Collection>> collectionsRepositoryStub;
        private Mock<IRepository<Tweet>> tweetsRepositoryStub;
        private Mock<IRepository<CollectionTweet>> collectionTweetsRepositoryStub;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            this.saverStub = new Mock<ISaver>();
            this.mapperStub = new Mock<IMappingProvider>();
            this.collectionsRepositoryStub = new Mock<IRepository<Collection>>();
            this.tweetsRepositoryStub = new Mock<IRepository<Tweet>>();
            this.collectionTweetsRepositoryStub = new Mock<IRepository<CollectionTweet>>();
        }

        [TestMethod]
        public void Throw_InvalidCollectionIdException_When_CollectionIdIsNull()
        {
            // Arrange
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidCollectionIdException>(() =>
            {
                collectionServie.RemoveTweetFromCollection(null, "tweetId");
            });
        }

        [TestMethod]
        public void Throw_InvalidCollectionIdException_When_CollectionIdIsEmpty()
        {
            // Arrange
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidCollectionIdException>(() =>
            {
                collectionServie.RemoveTweetFromCollection(" ", "tweetId");
            });
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsNull()
        {
            // Arrange
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidTweetIdException>(() =>
            {
                collectionServie.RemoveTweetFromCollection("collectionId", null);
            });
        }

        [TestMethod]
        public void Throw_InvalidTweetIdException_When_TweetIdIsEmpty()
        {
            // Arrange
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidTweetIdException>(() =>
            {
                collectionServie.RemoveTweetFromCollection("collectionId", " ");
            });
        }

        [TestMethod]
        public void Throw_NullCollectionException_When_CollectionIsNotFound()
        {
            // Arrange
            var collectionServie = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionsRepositoryStub.Object,
                this.tweetsRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<NullCollectionException>(() =>
            {
                collectionServie.RemoveTweetFromCollection("collectionId", "tweetId");
            });
        }

        [TestMethod]
        public void Throw_NullTweetException_When_TweetIsNotFound()
        {
            // Arrange
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

            // Act & Assert
            Assert.ThrowsException<NullTweetException>(() =>
            {
                collectionServie.RemoveTweetFromCollection("collectionId", "tweetId");
            });
        }

        [TestMethod]
        public void Call_CollectionRepository_All_Once()
        {
            // Arrange
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

            // Act
            collectionServie.RemoveTweetFromCollection("collectionId", "tweetId");

            // Assert
            this.collectionsRepositoryStub.Verify(
                x => x.All, 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_TweetRepository_All_Once()
        {
            // Arrange
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

            // Act
            collectionServie.RemoveTweetFromCollection("collectionId", "tweetId");

            // Assert
            this.tweetsRepositoryStub.Verify(
                x => x.All, 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_CollectionTweetRepository_Delete_Once()
        {
            // Arrange
            this.collectionTweetsRepositoryStub
                .Setup(x => x.Delete(It.IsAny<CollectionTweet>()))
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

            // Act
            collectionServie.RemoveTweetFromCollection("collectionId", "tweetId");

            // Assert
            this.collectionTweetsRepositoryStub.Verify(
                x => x.Delete(It.IsAny<CollectionTweet>()), 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_Saver_SaveChanges_Once()
        {
            // Arrange
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

            // Act
            collectionServie.RemoveTweetFromCollection("collectionId", "tweetId");

            // Assert
            this.saverStub.Verify(
                x => x.SaveChanges(), 
                Times.Once
            );
        }

        [TestMethod]
        public void Return_One_When_SuccessfullyAddedUserToFavourites()
        {
            // Arrange
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

            // Act & Assert
            Assert.AreEqual(
                1,
                collectionServie.RemoveTweetFromCollection("collectionId", "tweetId")
            );
        }
    }
}