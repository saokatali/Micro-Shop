using Catalog.API.Application.Messages.Commands.Catalog;
using Catalog.API.Common;
using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using Microsoft.Extensions.Options;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace Catalog.UnitTests.Application.Messages.Commands.Catalog
{
    public class DeleteTests
    {
        private Delete.Handler sut;
        private CatalogDataContext dataContext;
        private Guid id = Guid.NewGuid();

        public DeleteTests()
        {
            AppSettings appSettings = new AppSettings { IsTest=true};
            var options = new Mock<IOptionsSnapshot<AppSettings>>();
            options.Setup(x => x.Value).Returns(appSettings);
            dataContext = new CatalogDataContext(options.Object);
            dataContext.Products.AddRange(new List<Product> { new Product { Id=id } });


        }



        [Fact]
        public async void DeleteShouldDeleteTheRecord()
        {
            // Arrange
            sut = new Delete.Handler(dataContext);


            // Act
            var result = await sut.Handle(new Delete.Command { Id = id }, new System.Threading.CancellationToken());

            // Assert
            Assert.NotNull(result);
        }
    }
}
