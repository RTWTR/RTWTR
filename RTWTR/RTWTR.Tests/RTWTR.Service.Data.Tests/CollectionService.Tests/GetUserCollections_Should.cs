using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.CollectionService.Tests
{
    [TestClass]
    public class GetUserCollections_Should
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
        public void ReturnNull_When_InvokedWithNullUserId()
        {
            // Arrange
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionRepositoryStub.Object,
                this.tweetRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.ThrowsException<InvalidUserIdException>(() =>
            {
                collectionService.GetUserCollections(null);
            });
        }

        [TestMethod]
        public void ReturnZeroCollections_When_UserHasNoCollections()
        {
            // Arrange
            this.mapperStub
                .Setup(x => x.ProjectTo<CollectionDTO>(It.IsAny<IQueryable<object>>()))
                .Returns(new List<CollectionDTO>().AsQueryable());

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionRepositoryStub.Object,
                this.tweetRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act & Assert
            Assert.AreEqual(
                0,
                collectionService.GetUserCollections("userId").Count()
            );
        }

        [TestMethod]
        public void Call_CollectionRepository_All_Once()
        {
            // Arrange
            this.collectionRepositoryStub
                .Setup(x => x.All)
                .Returns(new List<Collection>()
                {
                    new Collection() { UserId = "userId"}
                }.AsQueryable())
                .Verifiable();
            
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionRepositoryStub.Object,
                this.tweetRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act
            collectionService.GetUserCollections("userId");

            // Assert
            this.collectionRepositoryStub.Verify(
                x => x.All,
                Times.Once
            );
        }

        [TestMethod]
        public void Call_Mapper_ProjectTo_Once()
        {
            // Arrange
            this.collectionRepositoryStub
                .Setup(x => x.All)
                .Returns(new List<Collection>()
                {
                    new Collection() { UserId = "userId"}
                }.AsQueryable());
            
            this.mapperStub
                .Setup(x => x.ProjectTo<CollectionDTO>(It.IsAny<IQueryable<Collection>>()))
                .Verifiable();
            
            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionRepositoryStub.Object,
                this.tweetRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act
            collectionService.GetUserCollections("userId");

            // Assert
            this.mapperStub.Verify(
                x => x.ProjectTo<CollectionDTO>(It.IsAny<IQueryable<Collection>>()),
                Times.Once
            );
        }

        [TestMethod]
        public void ReturnCorrectCollections_When_UserIsValid()
        {
            // Arrange
            this.collectionRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<Collection>()
                    {
                        new Collection() { Name = "nope", UserId = "notThisOne" },
                        new Collection() { Name = "test1", UserId = "userId" },
                        new Collection() { Name = "test2", UserId = "userId" }
                    }.AsQueryable());

            this.mapperStub
                .Setup(x => x.ProjectTo<CollectionDTO>(It.IsAny<IQueryable<object>>()))
                .Returns(
                    new List<CollectionDTO>()
                    {
                        new CollectionDTO() { Name = "test1" },
                        new CollectionDTO() { Name = "test2" }
                    }.AsQueryable());

            var collectionService = new global::RTWTR.Service.Data.CollectionService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.collectionRepositoryStub.Object,
                this.tweetRepositoryStub.Object,
                this.collectionTweetsRepositoryStub.Object
            );

            // Act
            var collections = collectionService.GetUserCollections("userId").ToList();

            // Assert
            Assert.AreEqual(
                2,
                collections.Count()
            );

            Assert.AreEqual(
                "test1",
                collections[0].Name
            );

            Assert.AreEqual(
                "test2",
                collections[1].Name
            );
        }
    }
}