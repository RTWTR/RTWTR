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
        [TestMethod]
        public void NotReturnNull_When_Invoked()
        {
            //Arrange
            var saverMock = new Mock<ISaver>();
            var mapperMock = new Mock<IMappingProvider>();
            var tweetRepositoryMock = new Mock<IRepository<Tweet>>();
            var collectionRepositoryMock = new Mock<IRepository<Collection>>();
            var collectionTweetsRepositoryMock = new Mock<IRepository<CollectionTweet>>();


            //Action
            var collectionService = new global::RTWTR.Service.Data.CollectionService(saverMock.Object, mapperMock.Object,
                collectionRepositoryMock.Object, tweetRepositoryMock.Object, collectionTweetsRepositoryMock.Object);

            //Assert
            Assert.IsNotNull(collectionService);
        }
    }
}
