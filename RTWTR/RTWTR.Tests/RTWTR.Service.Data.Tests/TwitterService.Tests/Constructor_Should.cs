using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using RTWTR.Infrastructure.Contracts;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Twitter.Contracts;

namespace RTWTR.Tests.RTWTR.Service.Data.Tests.TwitterService.Tests
{
    [TestClass]
    public class Constructor_Should
    {
        private Mock<IApiProvider> apiProviderStub;
        private Mock<IJsonProvider> jsonProviderStub;
        private Mock<IMappingProvider> mappingProviderStub;

        [TestInitialize]
        public void TestInitialize()
        {
            this.apiProviderStub = new Mock<IApiProvider>();
            this.jsonProviderStub = new Mock<IJsonProvider>();
            this.mappingProviderStub = new Mock<IMappingProvider>();
        }

        [TestMethod]
        public void Not_ReturnNull_When_InvokedWithCorrectParameters()
        {
            // Arrange & Act
            var twitterService = new global::RTWTR.Service.Twitter.TwitterService(
                this.apiProviderStub.Object,
                this.jsonProviderStub.Object,
                this.mappingProviderStub.Object
            );

            // Assert
            Assert.IsNotNull(twitterService);
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_ApiProvider_IsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Twitter.TwitterService(
                    null,
                    this.jsonProviderStub.Object,
                    this.mappingProviderStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_JsonProvider_IsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Twitter.TwitterService(
                    this.apiProviderStub.Object,
                    null,
                    this.mappingProviderStub.Object
                );
            });
        }

        [TestMethod]
        public void Throw_ArgumentNullException_When_MappingProvider_IsNull()
        {
            // Arrange & Act & Assert
            Assert.ThrowsException<ArgumentNullException>(() =>
            {
                new global::RTWTR.Service.Twitter.TwitterService(
                    this.apiProviderStub.Object,
                    this.jsonProviderStub.Object,
                    null
                );
            });
        }
    }
}
