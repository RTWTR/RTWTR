// using System;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Text;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using RTWTR.Data.Access.Contracts;
// using RTWTR.Data.Models;
// using RTWTR.DTO;
// using RTWTR.Infrastructure.Mapping.Provider;

// namespace RTWTR.Tests.RTWTR.Service.Data.Tests.CollectionService.Tests
// {
//     [TestClass]
//     public class GetUserCollections_Should
//     {
//         private Mock<ISaver> saverStub;
//         private Mock<IMappingProvider> mapperStub;
//         private Mock<IRepository<Tweet>> tweetRepositoryStub;
//         private Mock<IRepository<Collection>> collectionRepositoryStub;
//         private Mock<IRepository<CollectionTweet>> collectionTweetsRepositoryStub;

//         [TestInitialize]
//         public void TestInitialize()
//         {
//             // Arrange
//             this.saverStub = new Mock<ISaver>();
//             this.mapperStub = new Mock<IMappingProvider>();
//             this.tweetRepositoryStub = new Mock<IRepository<Tweet>>();
//             this.collectionRepositoryStub = new Mock<IRepository<Collection>>();
//             this.collectionTweetsRepositoryStub = new Mock<IRepository<CollectionTweet>>();
//         }

//         [TestMethod]
//         public void ReturnNull_When_InvokedWithNullUserId()
//         {
//             // Arrange
//             var collectionService = new global::RTWTR.Service.Data.CollectionService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.collectionRepositoryStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.collectionTweetsRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.IsNull(collectionService.GetUserCollections(null));
//         }

//         [TestMethod]
//         public void ReturnZeroCollections_When_UserHasNoCollections()
//         {
//             // Arrange
//             this.collectionRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<Collection>()
//                     {
//                         new Collection() { Name = "testCollection", UserId = "notThisOne" }
//                     }.AsQueryable()
//                 );

//             this.mapperStub
//                 .Setup(x => x.ProjectTo<CollectionDTO>(It.IsAny<IQueryable<object>>()))
//                 .Returns(new List<CollectionDTO>().AsQueryable());

//             var collectionService = new global::RTWTR.Service.Data.CollectionService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.collectionRepositoryStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.collectionTweetsRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.AreEqual(
//                 0,
//                 collectionService.GetUserCollections("1234567890").Count()
//             );
//         }

//         [TestMethod]
//         public void ReturnCorrectCollections_When_UserIsValid()
//         {
//             // Arrange
//             this.collectionRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<Collection>()
//                     {
//                         new Collection() { Name = "nope", UserId = "notThisOne" },
//                         new Collection() { Name = "test1", UserId = "1234567890" },
//                         new Collection() { Name = "test2", UserId = "1234567890" }
//                     }.AsQueryable());

//             this.mapperStub
//                 .Setup(x => x.ProjectTo<CollectionDTO>(It.IsAny<IQueryable<object>>()))
//                 .Returns(
//                     new List<CollectionDTO>()
//                     {
//                         new CollectionDTO() { Name = "test1" },
//                         new CollectionDTO() { Name = "test2" }
//                     }.AsQueryable());

//             var collectionService = new global::RTWTR.Service.Data.CollectionService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.collectionRepositoryStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.collectionTweetsRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.AreEqual(
//                 2,
//                 collectionService.GetUserCollections("1234567890").Count()
//             );
//         }
//     }
// }