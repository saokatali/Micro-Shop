using Catalog.API.Application.Messages.Commands.Catalog;
using Moq;
using System;
using Xunit;

namespace Catalog.UnitTests.Application.Messages.Commands.Catalog
{
    public class DeleteTests
    {
        private readonly MockRepository mockRepository;



        public DeleteTests()
        {
            this.mockRepository = new MockRepository(MockBehavior.Strict);


        }

        private Delete CreateDelete()
        {
            return new Delete();
        }

        [Fact]
        public void TestMethod1()
        {
            // Arrange
            var delete = this.CreateDelete();

            // Act


            // Assert
            Assert.True(false);
            this.mockRepository.VerifyAll();
        }
    }
}
