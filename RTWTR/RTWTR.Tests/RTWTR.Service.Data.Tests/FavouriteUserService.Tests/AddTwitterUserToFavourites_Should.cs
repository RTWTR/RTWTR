using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.FavouriteUserService.Tests
{
    [TestClass]
    public class AddTwitterUserToFavourites_Should
    {
        private Mock<ISaver> saverStub;
        private Mock<IMappingProvider> mapperStub;
        private Mock<IRepository<User>> userRepositoryStub;
        private Mock<IRepository<TwitterUser>> twitterUserRepositoryStub;
        private Mock<IRepository<UserTwitterUser>> userTwitterUserRepositoryStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.saverStub = new Mock<ISaver>();
            this.mapperStub = new Mock<IMappingProvider>();
            this.userRepositoryStub = new Mock<IRepository<User>>();
            this.twitterUserRepositoryStub = new Mock<IRepository<TwitterUser>>();
            this.userTwitterUserRepositoryStub = new Mock<IRepository<UserTwitterUser>>();
        }

        [TestMethod]
        public void Throw_InvalidUserIdException_When_UserIdIsNull()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidUserIdException>(() =>
            {
                favouriteUserService.AddTwitterUserToFavourites(null, "twitterUserId");
            });
        }

        [TestMethod]
        public void Throw_InvalidUserIdException_When_UserIdIsEmpty()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidUserIdException>(() =>
            {
                favouriteUserService.AddTwitterUserToFavourites(" ", "twitterUserId");
            });
        }

        [TestMethod]
        public void Throw_InvalidTwitterUserIdException_When_TwitterUserIdIsNull()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidTwitterUserIdException>(() =>
            {
                favouriteUserService.AddTwitterUserToFavourites("userId", null);
            });
        }

        [TestMethod]
        public void Throw_InvalidTwitterUserIdException_When_TwitterUserIdIsEmpty()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.ThrowsException<InvalidTwitterUserIdException>(() =>
            {
                favouriteUserService.AddTwitterUserToFavourites("userId", " ");
            });
        }

        [TestMethod]
        public void Throw_NullUserException_When_UserIsNotFound()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.ThrowsException<NullUserException>(() =>
            {
                favouriteUserService.AddTwitterUserToFavourites("userId", "twitterId");
            });
        }

        [TestMethod]
        public void Throw_NullTwitterUserException_When_TwitterUserIsNotFound()
        {
            this.userRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<User>()
                    {
                        new User() { Id = "userId" }
                    }.AsQueryable()
                );

            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.ThrowsException<NullTwitterUserException>(() =>
            {
                favouriteUserService.AddTwitterUserToFavourites("userId", "twitterId");
            });
        }

        [TestMethod]
        public void Call_UserRepository_All_Once()
        {
            this.userRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<User>()
                    {
                        new User() { Id = "userId" }
                    }.AsQueryable()
                )
                .Verifiable();
            
            this.twitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<TwitterUser>()
                    {
                        new TwitterUser() { Id = "twitterUserId" }
                    }.AsQueryable()
                );

            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            favouriteUserService.AddTwitterUserToFavourites("userId", "twitterUserId");

            this.userRepositoryStub.Verify(
                x => x.All, 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_TwitterUserRepository_All_Once()
        {
            this.userRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<User>()
                    {
                        new User() { Id = "userId" }
                    }.AsQueryable()
                );
            
            this.twitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<TwitterUser>()
                    {
                        new TwitterUser() { Id = "twitterUserId" }
                    }.AsQueryable()
                )
                .Verifiable();

            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            favouriteUserService.AddTwitterUserToFavourites("userId", "twitterUserId");

            this.twitterUserRepositoryStub.Verify(
                x => x.All, 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_UserTwitterUserRepository_Add_Once()
        {
            this.userTwitterUserRepositoryStub
                .Setup(x => x.Add(It.IsAny<UserTwitterUser>()))
                .Verifiable();

            this.userRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<User>()
                    {
                        new User() { Id = "userId" }
                    }.AsQueryable()
                );
            
            this.twitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<TwitterUser>()
                    {
                        new TwitterUser() { Id = "twitterUserId" }
                    }.AsQueryable()
                );

            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            favouriteUserService.AddTwitterUserToFavourites("userId", "twitterUserId");

            this.userTwitterUserRepositoryStub.Verify(
                x => x.Add(It.IsAny<UserTwitterUser>()), 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_Saver_SaveChanges_Once()
        {
            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1)
                .Verifiable();

            this.userRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<User>()
                    {
                        new User() { Id = "userId" }
                    }.AsQueryable()
                );
            
            this.twitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<TwitterUser>()
                    {
                        new TwitterUser() { Id = "twitterUserId" }
                    }.AsQueryable()
                );

            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            favouriteUserService.AddTwitterUserToFavourites("userId", "twitterUserId");

            this.saverStub.Verify(
                x => x.SaveChanges(), 
                Times.Once
            );
        }

        [TestMethod]
        public void Return_One_When_SuccessfullyAddedUserToFavourites()
        {
            this.saverStub
                .Setup(x => x.SaveChanges())
                .Returns(1);

            this.userRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<User>()
                    {
                        new User() { Id = "userId" }
                    }.AsQueryable()
                );
            
            this.twitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<TwitterUser>()
                    {
                        new TwitterUser() { Id = "twitterUserId" }
                    }.AsQueryable()
                );

            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.AreEqual(
                1,
                favouriteUserService.AddTwitterUserToFavourites("userId", "twitterUserId")
            );
        }
    }
}