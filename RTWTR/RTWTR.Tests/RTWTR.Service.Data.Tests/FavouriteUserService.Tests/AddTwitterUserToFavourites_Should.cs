using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.FavouriteUserService.Tests
{
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
        public void Return_MinusOne_When_UserIdIsNull()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.AreEqual(-1, favouriteUserService.AddTwitterUserToFavorites(null, "twitterUserId"));
        }

        [TestMethod]
        public void Return_MinusOne_When_UserIdIsEmpty()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.AreEqual(-1, favouriteUserService.AddTwitterUserToFavorites(" ", "twitterUserId"));
        }

        [TestMethod]
        public void Return_MinusOne_When_TwitterUserIdIsNull()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.AreEqual(-1, favouriteUserService.AddTwitterUserToFavorites("userId", null));
        }

        [TestMethod]
        public void Return_MinusOne_When_TwitterUserIdIsEmpty()
        {
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            Assert.AreEqual(-1, favouriteUserService.AddTwitterUserToFavorites("userId", " "));
        }
    }
}