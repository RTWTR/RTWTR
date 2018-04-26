using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping.Provider;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.FavouriteUserService.Tests
{
    [TestClass]
    public class Constructor_Should
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
        public void NotReturnNull_When_InvokedWithCorrectParameters()
        {
            // Arrange
            var favouriteUserService = new global::RTWTR.Service.Data.FavouriteUserService(
                this.saverStub.Object,
                this.mapperStub.Object,
                this.userRepositoryStub.Object,
                this.twitterUserRepositoryStub.Object,
                this.userTwitterUserRepositoryStub.Object
            );

            // Act & Assert
            Assert.IsNotNull(favouriteUserService);
        }

        [TestMethod]
        public void Throw_ArgumentNullExceptio_When_SaverIsNull()
        {
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.FavouriteUserService(
                    null,
                    this.mapperStub.Object,
                    this.userRepositoryStub.Object,
                    this.twitterUserRepositoryStub.Object,
                    this.userTwitterUserRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullExceptio_When_MapperIsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.FavouriteUserService(
                    this.saverStub.Object,
                    null,
                    this.userRepositoryStub.Object,
                    this.twitterUserRepositoryStub.Object,
                    this.userTwitterUserRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullExceptio_When_UserIsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.FavouriteUserService(
                    this.saverStub.Object,
                    this.mapperStub.Object,
                    null,
                    this.twitterUserRepositoryStub.Object,
                    this.userTwitterUserRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullExceptio_When_TwitterUsersIsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.FavouriteUserService(
                    this.saverStub.Object,
                    this.mapperStub.Object,
                    this.userRepositoryStub.Object,
                    null,
                    this.userTwitterUserRepositoryStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullExceptio_When_UserTwitterUsersIsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Data.FavouriteUserService(
                    this.saverStub.Object,
                    this.mapperStub.Object,
                    this.userRepositoryStub.Object,
                    this.twitterUserRepositoryStub.Object,
                    null
                );
            });
        }
    }
}