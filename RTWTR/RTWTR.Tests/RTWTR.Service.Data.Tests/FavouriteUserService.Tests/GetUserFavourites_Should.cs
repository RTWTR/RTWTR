using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure.Exceptions;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.FavouriteUserService.Tests
{
    [TestClass]
    public class GetUserFavourites_Should
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
                favouriteUserService.GetUserFavourites(null);
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
                favouriteUserService.GetUserFavourites(" ");
            });
        }

        [TestMethod]
        public void Return_NoFavourites_When_UserHasNoFavourites()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            var favourites = favouriteUserService
                .GetUserFavourites("userId")
                .ToList();

            Assert.AreEqual(
                0,
                favourites.Count
            );
        }

        [TestMethod]
        public void Call_UserTwitterUserRepository_All_Once()
        {
            this.userTwitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<UserTwitterUser>()
                    {
                        new UserTwitterUser() { UserId = "userId" }
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

            favouriteUserService.GetUserFavourites("userId");

            this.userTwitterUserRepositoryStub.Verify(
                x => x.All, 
                Times.Once
            );
        }

        [TestMethod]
        public void Call_Mapper_ProjectTo_Once()
        {
            this.userTwitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<UserTwitterUser>()
                    {
                        new UserTwitterUser() { UserId = "userId" }
                    }.AsQueryable()
                );

            this.mapperStub
                .Setup(x => x.ProjectTo<TwitterUserDto>(It.IsAny<IQueryable<UserTwitterUser>>()))
                .Returns(
                    new List<TwitterUserDto>()
                    {
                        new TwitterUserDto() { Name = "twitterUser" }
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

            favouriteUserService.GetUserFavourites("userId");

            this.mapperStub.Verify(
                x => x.ProjectTo<TwitterUserDto>(It.IsAny<IQueryable<UserTwitterUser>>()), 
                Times.Once
            );
        }

        [TestMethod]
        public void Return_UserFavourites_When_UserIdIsValid()
        {
            this.userTwitterUserRepositoryStub
                .Setup(x => x.All)
                .Returns(
                    new List<UserTwitterUser>()
                    {
                        new UserTwitterUser() { UserId = "userId" }
                    }.AsQueryable()
                );

            this.mapperStub
                .Setup(x => x.ProjectTo<TwitterUserDto>(It.IsAny<IQueryable<UserTwitterUser>>()))
                .Returns(
                    new List<TwitterUserDto>()
                    {
                        new TwitterUserDto() { Name = "twitterUser" }
                    }.AsQueryable()
                );

            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            var favourites = favouriteUserService
                .GetUserFavourites("userId")
                .ToList();

            Assert.AreEqual(
                1,
                favourites.Count()
            );
            
            Assert.AreEqual(
                "twitterUser",
                favourites[0].Name
            );
        }
    }
}
