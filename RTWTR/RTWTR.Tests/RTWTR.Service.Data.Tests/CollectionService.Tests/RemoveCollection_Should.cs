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
    public class RemoveCollection_Should
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
        public void Throw_InvalidCollectionIdException_When_InvokedWithNullCollectionId()
        {
            // Arrange
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidCollectionIdException>(() =>
            {
                collectionService.RemoveCollection(null);
            });
        }

        [TestMethod]
        public void Throw_InvalidCollectionIdException_When_InvokedWithEmptyCollectionId()
        {
            // Arrange
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidCollectionIdException>(() =>
            {
                collectionService.RemoveCollection(" ");
            });
        }

        [TestMethod]
        public void Throw_NullCollectionException_When_CollectionIsNotFound()
        {
            // Arrange
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<NullCollectionException>(() =>
            {
                collectionService.RemoveCollection("collectionId");
            });
        }

        [TestMethod]
        public void Call_CollectionRepository_All_Once()
        {
            // Arrange
            this.collectionRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                )
                .Verifiable();

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act
            collectionService.RemoveCollection("collectionId");

            // Assert
            this.collectionRepositoryStub.Verify(
                x => x.All,
                Times.Once
            );
        }

        [TestMethod]
        public void Call_CollectionRepository_Delete_Once()
        {
            // Arrange
            this.collectionRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Id = "collectionId" }
                    }.AsQueryable()
                );
            
            this.collectionRepositoryStub
                .Setup(x => x.Delete(It.IsAny<Collection>()))
                .Verifiable();

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act
            collectionService.RemoveCollection("collectionId");

            // Assert
            this.collectionRepositoryStub.Verify(
                x => x.Delete(It.IsAny<Collection>()),
                Times.Once
            );
        }

        [TestMethod]
        public void Call_Saver_SaveChanges_Once()
        {
            // Arrange
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
                .Verifiable();

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                saverStub.Object,
                mapperStub.Object,
                collectionRepositoryStub.Object,
                tweetRepositoryStub.Object,
                collectionTweetsRepositoryStub.Object
            );

            // Act
            collectionService.RemoveCollection("collectionId");

            // Assert
            this.saverStub.Verify(
                x => x.SaveChanges(),
                Times.Once
            );
        }

        [TestMethod]
        public void ReturnOne_When_CollectionSuccessfullyRemoved()
        {
            // Arrange
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

            // Act & Assert
            Assert.AreEqual(
                1, 
                collectionService.RemoveCollection("collectionId")
            );
        }
    }
}