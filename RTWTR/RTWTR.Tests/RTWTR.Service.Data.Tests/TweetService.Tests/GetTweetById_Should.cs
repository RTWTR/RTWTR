// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using RTWTR.Data.Access.Contracts;
// using RTWTR.Data.Models;
// using RTWTR.DTO;
// using RTWTR.Infrastructure.Mapping.Provider;

// namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TweetService.Tests
// {
//     [TestClass]
//     public class GetTweetById_Should
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
//         public void ReturnNull_When_TweetIdIsNull()
//         {
//             // Arrange
//             var tweetService = new global::RTWTR.Service.Data.TweetService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.userStub.Object,
//                 this.userTweetsStub.Object
//             );

//             // Act & Assert
//             Assert.IsNull(tweetService.GetTweetById(null));
//         }

//         [TestMethod]
//         public void ReturnNull_When_TweetIdIsEmpty()
//         {
//             // Arrange
//             var tweetService = new global::RTWTR.Service.Data.TweetService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.userStub.Object,
//                 this.userTweetsStub.Object
//             );

//             // Act & Assert
//             Assert.IsNull(tweetService.GetTweetById(""));
//         }

//         [TestMethod]
//         public void ReturnNull_When_TweetIsNotFound()
//         {
//             // Arrange
//             this.tweetRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<Tweet>()
//                     {
//                         new Tweet() { Id = "notThisOne" }
//                     }.AsQueryable()
//                 );

//             var tweetService = new global::RTWTR.Service.Data.TweetService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.userStub.Object,
//                 this.userTweetsStub.Object
//             );

//             // Act & Assert
//             Assert.IsNull(tweetService.GetTweetById("notFound"));
//         }

//         [TestMethod]
//         public void ReturnCorrectTweetDTO_When_TweetIdIsValid()
//         {
//             // Arrange
//             this.tweetRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<Tweet>()
//                     {
//                         new Tweet() { Id = "tweetId", Text = "text" }
//                     }.AsQueryable()
//                 );

//             var expectedTweetDTO = new TweetDto() { Text = "text" };

//             this.mapperStub
//                 .Setup(x => x.MapTo<TweetDto>(It.IsAny<Tweet>()))
//                 .Returns(expectedTweetDTO);

//             var tweetService = new global::RTWTR.Service.Data.TweetService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.tweetRepositoryStub.Object,
//                 this.userStub.Object,
//                 this.userTweetsStub.Object
//             );

//             // Act
//             var returnedTweetDTO = tweetService.GetTweetById("tweetId");

//             // Assert
//             Assert.IsNotNull(returnedTweetDTO);
//             Assert.AreEqual(expectedTweetDTO.Id, returnedTweetDTO.Id);
//             Assert.AreEqual(expectedTweetDTO.Text, returnedTweetDTO.Text);
//         }
//     }
// }