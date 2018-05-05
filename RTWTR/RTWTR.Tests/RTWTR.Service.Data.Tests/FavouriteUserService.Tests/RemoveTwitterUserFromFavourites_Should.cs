// using System;
// using System.Collections.Generic;
// using System.Linq;
// using Microsoft.VisualStudio.TestTools.UnitTesting;
// using Moq;
// using RTWTR.Data.Access.Contracts;
// using RTWTR.Data.Models;
// using RTWTR.Infrastructure.Exceptions;
// using RTWTR.Infrastructure.Mapping.Provider;

// namespace RTWTR.Tests.RTWTR.Service.Data.Tests.FavouriteUserService.Tests
// {
//     [TestClass]
//     public class RemoveTwitterUserFromFavourites_Should
//     {
//         private Mock<ISaver> saverStub;
//         private Mock<IMappingProvider> mapperStub;
//         private Mock<IRepository<User>> userRepositoryStub;
//         private Mock<IRepository<TwitterUser>> twitterUserRepositoryStub;
//         private Mock<IRepository<UserTwitterUser>> userTwitterUserRepositoryStub;

//         [TestInitialize]
//         public void TestInitialize()
//         {
//             // Arrange
//             this.saverStub = new Mock<ISaver>();
//             this.mapperStub = new Mock<IMappingProvider>();
//             this.userRepositoryStub = new Mock<IRepository<User>>();
//             this.twitterUserRepositoryStub = new Mock<IRepository<TwitterUser>>();
//             this.userTwitterUserRepositoryStub = new Mock<IRepository<UserTwitterUser>>();
//         }

//         [TestMethod]
//         public void Throw_InvalidUserIdException_When_UserIdIsNull()
//         {
//             // Arrange
//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.ThrowsException<InvalidUserIdException>(() =>
//             {
//                 favouriteUserService.RemoveTwitterUserFromFavourites(null, "twitterUserId");
//             });
//         }

//         [TestMethod]
//         public void Throw_InvalidUserIdException_When_UserIdIsEmpty()
//         {
//             // Arrange
//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.ThrowsException<InvalidUserIdException>(() =>
//             {
//                 favouriteUserService.RemoveTwitterUserFromFavourites(" ", "twitterUserId");
//             });
//         }

//         [TestMethod]
//         public void Throw_InvalidTwitterUserIdException_When_TwitterUserIdIsNull()
//         {
//             // Arrange
//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.ThrowsException<InvalidTwitterUserIdException>(() =>
//             {
//                 favouriteUserService.RemoveTwitterUserFromFavourites("userId", null);
//             });
//         }

//         [TestMethod]
//         public void Throw_InvalidTwitterUserIdException_When_TwitterUserIdIsEmpty()
//         {
//             // Arrange
//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.ThrowsException<InvalidTwitterUserIdException>(() =>
//             {
//                 favouriteUserService.RemoveTwitterUserFromFavourites("userId", " ");
//             });
//         }

//         [TestMethod]
//         public void Throw_NullUserException_When_UserIsNotFound()
//         {
//             // Arrange
//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.ThrowsException<NullUserException>(() =>
//             {
//                 favouriteUserService.RemoveTwitterUserFromFavourites("userId", "twitterId");
//             });
//         }

//         [TestMethod]
//         public void Throw_NullTwitterUserException_When_TwitterUserIsNotFound()
//         {
//             // Arrange
//             this.userRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<User>()
//                     {
//                         new User() { Id = "userId" }
//                     }.AsQueryable()
//                 );

//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.ThrowsException<NullTwitterUserException>(() =>
//             {
//                 favouriteUserService.RemoveTwitterUserFromFavourites("userId", "twitterId");
//             });
//         }

//         [TestMethod]
//         public void Call_UserRepository_All_Once()
//         {
//             // Arrange
//             this.userRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<User>()
//                     {
//                         new User() { Id = "userId" }
//                     }.AsQueryable()
//                 )
//                 .Verifiable();
            
//             this.twitterUserRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<TwitterUser>()
//                     {
//                         new TwitterUser() { Id = "twitterUserId" }
//                     }.AsQueryable()
//                 );

//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act
//             favouriteUserService.AddTwitterUserToFavourites("userId", "twitterUserId");

//             // Assert
//             this.userRepositoryStub.Verify(
//                 x => x.All, 
//                 Times.Once
//             );
//         }

//         [TestMethod]
//         public void Call_TwitterUserRepository_All_Once()
//         {
//             // Arrange
//             this.userRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<User>()
//                     {
//                         new User() { Id = "userId" }
//                     }.AsQueryable()
//                 );
            
//             this.twitterUserRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<TwitterUser>()
//                     {
//                         new TwitterUser() { Id = "twitterUserId" }
//                     }.AsQueryable()
//                 )
//                 .Verifiable();

//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act
//             favouriteUserService.AddTwitterUserToFavourites("userId", "twitterUserId");

//             // Assert
//             this.twitterUserRepositoryStub.Verify(
//                 x => x.All, 
//                 Times.Once
//             );
//         }

//         [TestMethod]
//         public void Call_UserTwitterUserRepository_Delete_Once()
//         {
//             // Arrange
//             this.userTwitterUserRepositoryStub
//                 .Setup(x => x.Delete(It.IsAny<UserTwitterUser>()))
//                 .Verifiable();

//             this.userRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<User>()
//                     {
//                         new User() { Id = "userId" }
//                     }.AsQueryable()
//                 );
            
//             this.twitterUserRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<TwitterUser>()
//                     {
//                         new TwitterUser() { Id = "twitterUserId" }
//                     }.AsQueryable()
//                 );

//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act
//             favouriteUserService.RemoveTwitterUserFromFavourites("userId", "twitterUserId");

//             // Assert
//             this.userTwitterUserRepositoryStub.Verify(
//                 x => x.Delete(It.IsAny<UserTwitterUser>()), 
//                 Times.Once
//             );
//         }

//         [TestMethod]
//         public void Call_Saver_SaveChanges_Once()
//         {
//             // Arrange
//             this.saverStub
//                 .Setup(x => x.SaveChanges())
//                 .Returns(1)
//                 .Verifiable();

//             this.userRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<User>()
//                     {
//                         new User() { Id = "userId" }
//                     }.AsQueryable()
//                 );
            
//             this.twitterUserRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<TwitterUser>()
//                     {
//                         new TwitterUser() { Id = "twitterUserId" }
//                     }.AsQueryable()
//                 );

//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act
//             favouriteUserService.RemoveTwitterUserFromFavourites("userId", "twitterUserId");

//             // Assert
//             this.saverStub.Verify(
//                 x => x.SaveChanges(), 
//                 Times.Once
//             );
//         }

//         [TestMethod]
//         public void Return_One_When_SuccessfullyRemovedUserFromFavourites()
//         {
//             // Arrange
//             this.saverStub
//                 .Setup(x => x.SaveChanges())
//                 .Returns(1);

//             this.userRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<User>()
//                     {
//                         new User() { Id = "userId" }
//                     }.AsQueryable()
//                 );
            
//             this.twitterUserRepositoryStub
//                 .Setup(x => x.All)
//                 .Returns(
//                     new List<TwitterUser>()
//                     {
//                         new TwitterUser() { Id = "twitterUserId" }
//                     }.AsQueryable()
//                 );

//             var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
//                 this.saverStub.Object,
//                 this.mapperStub.Object,
//                 this.userRepositoryStub.Object,
//                 this.twitterUserRepositoryStub.Object,
//                 this.userTwitterUserRepositoryStub.Object
//             );

//             // Act & Assert
//             Assert.AreEqual(
//                 1,
//                 favouriteUserService.RemoveTwitterUserFromFavourites("userId", "twitterUserId")
//             );
//         }
//     }
// }
