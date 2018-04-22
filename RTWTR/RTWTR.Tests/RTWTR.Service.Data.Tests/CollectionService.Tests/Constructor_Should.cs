using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Data;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.CollectionService.Tests
{
    [TestClass]
    public class Constructor_Should
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
        public void NotReturnNull_When_InvokedWithCorrectParameters()
        {
            // Arrange & Act
            var collectionServiceMock = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionRepositoryStub.Object,
                this.tweetRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Assert
            Assert.IsNotNull(collectionServiceMock);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_SaverIsNull()
        {
            // Arrange & Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.CollectionService(
                    null,
                    this.mapperStub.Object,
                    this.collectionRepositoryStub.Object,
                    this.tweetRepositoryStub.Object,
                    this.collectionTweetsRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_MapperIsNull()
        {
            // Arrange & Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.CollectionService(
                    this.saverStub.Object,
                    null,
                    this.collectionRepositoryStub.Object,
                    this.tweetRepositoryStub.Object,
                    this.collectionTweetsRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_CollectionsIsNull()
        {
            // Arrange & Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.CollectionService(
                    this.saverStub.Object,
                    this.mapperStub.Object,
                    null,
                    this.tweetRepositoryStub.Object,
                    this.collectionTweetsRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_TweetsIsNull()
        {
            // Arrange & Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.CollectionService(
                    this.saverStub.Object,
                    this.mapperStub.Object,
                    this.collectionRepositoryStub.Object,
                    null,
                    this.collectionTweetsRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_CollectionTweetsIsNull()
        {
            // Arrange & Act && Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.CollectionService(
                    this.saverStub.Object,
                    this.mapperStub.Object,
                    this.collectionRepositoryStub.Object,
                    this.tweetRepositoryStub.Object,
                    null
                );
            });
        }
    }
}