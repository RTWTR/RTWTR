using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;


namespace RTWTR.Tests.RTWTR.Service.Data.Tests.CollectionService.Tests
{
    [TestClass]
    public class RemoveTweetFromCollection_Should
    {
        [TestMethod]
        public void ReturnMinusOne_When_InvokedWithIncorrectId()
        {
            //Arrange
            var saverMock = new Mock<ISaver>();
            var mapperMock = new Mock<IMappingProvider>();
            var tweetRepositoryMock = new Mock<IRepository<Tweet>>();
            var collectionRepositoryMock = new Mock<IRepository<Collection>>();
            var collectionTweetsRepositoryMock = new Mock<IRepository<CollectionTweet>>();
            var collectionService = new global::RTWTR.Service.Data.CollectionService(saverMock.Object, mapperMock.Object,
                collectionRepositoryMock.Object, tweetRepositoryMock.Object, collectionTweetsRepositoryMock.Object);

            var expected = -1;
            //Action & Assert
            Assert.AreEqual(expected, collectionService.RemoveTweetFromCollection(null,null));
        }
    }
}
