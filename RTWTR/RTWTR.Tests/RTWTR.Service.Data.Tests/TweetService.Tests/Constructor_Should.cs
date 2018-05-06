// using System;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using RTWTR.Data.Access.Contracts;
// using RTWTR.Data.Models;
// using RTWTR.Infrastructure.Mapping.Provider;

// namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TweetService.Tests
// {
//     [TestClass]
//     public class Constructor_Should
//     {
//         private Mock<ISaver> saverStub;
//         private Mock<IMappingProvider> mapperStub;
//         private Mock<IRepository<Tweet>> tweetRepositoryStub;
//         private Mock<IRepository<User>> userStub;
//         private Mock<IRepository<UserTweets>> userTweetsStub;

//         [TestInitialize]
//         public void TestInitialize()
//         {
//             this.saverStub = new Mock<ISaver>();
//             this.mapperStub = new Mock<IMappingProvider>();
//             this.tweetRepositoryStub = new Mock<IRepository<Tweet>>();
//             this.userStub = new Mock<IRepository<User>>();
//             this.userTweetsStub = new Mock<IRepository<UserTweets>>();
//         }

//         [TestMethod]
//         public void NotReturnNull_When_InvokedWithCorrectParameters()
//         {
//             // Arrange & Act
//             var tweetService = new global::RTWTR.Service.Data.TweetService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.userStub.Object,
//                 this.userTweetsStub.Object


//             );

//             // Assert
//             Assert.IsNotNull(tweetService);
//         }

//         [TestMethod]
//         public void Throw_ArgumentNullException_When_SaverIsNull()
//         {
//             // Arrange & Act & Assert
//             Assert.ThrowsException<ArgumentNullException>(() =>
//             {
//                 new global::RTWTR.Service.Data.TweetService(
//                     null,
//                     this.mapperStub.Object,
//                     this.tweetRepositoryStub.Object,
//                     this.userStub.Object,
//                     this.userTweetsStub.Object
//                 );
//             });
//         }

//         [TestMethod]
//         public void Throw_ArgumentNullException_When_MappingProviderIsNull()
//         {
//             // Arrange & Act & Assert
//             Assert.ThrowsException<ArgumentNullException>(() =>
//             {
//                 new global::RTWTR.Service.Data.TweetService(
//                     this.saverStub.Object,
//                     null,
//                     this.tweetRepositoryStub.Object,
//                     this.userStub.Object,
//                     this.userTweetsStub.Object
//                 );
//             });
//         }

//         [TestMethod]
//         public void Throw_ArgumentNullException_When_TweetRepositoryIsNull()
//         {
//             // Arrange & Act & Assert
//             Assert.ThrowsException<ArgumentNullException>(() =>
//             {
//                 new global::RTWTR.Service.Data.TweetService(
//                     this.saverStub.Object,
//                     this.mapperStub.Object,
//                     null,
//                     this.userStub.Object,
//                     this.userTweetsStub.Object
//                 );
//             });
//         }
//     }
// }