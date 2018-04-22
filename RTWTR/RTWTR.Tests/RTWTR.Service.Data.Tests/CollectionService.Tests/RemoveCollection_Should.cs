using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
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

            Assert.AreEqual(-1, collectionService.RemoveCollection(null));
        }

        [TestMethod]
        public void ReturnMinusOne_When_CollectionIsNotFound()
        {
            //Arrange
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

            Assert.AreEqual(-1, collectionService.RemoveCollection("collectionId"));
        }

        [TestMethod]
        public void ReturnOne_When_CollectionSuccessfullyRemoved()
        {
            //Arrange
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

            Assert.AreEqual(1, collectionService.RemoveCollection("collectionId"));
        }
    }
}
