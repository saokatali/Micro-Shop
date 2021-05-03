using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Application.Messages.Commands.Catalog;
using Catalog.API.Common.Dto;
using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.InMemory;

using Moq;
using Xunit;

namespace Catalog.UnitTests.TestCases
{
    public class CatalogTests : IDisposable
    {
        Create.Handler handler;
      ProductDto  newProduct = new ProductDto { CaregoryIds= new List<Guid> { }};

public CatalogTests()
        {
            var mapper = new Mock<IMapper>();
            var product = new Product { };
            mapper.Setup(e => e.Map<Product>(newProduct)).Returns(product);

            var optionsBuilder = new DbContextOptionsBuilder<CatalogDataContext>().UseInMemoryDatabase("Catalog");
            var dbContext = new CatalogDataContext(optionsBuilder.Options);

            handler = new Create.Handler(dbContext, mapper.Object);

        }

        [Fact]
        public async Task CreateShouldCreateANewProduct()
        {

            var command = new Create.Command { Product= newProduct };
            var result = await handler.Handle(command, new CancellationToken());
            Assert.Equal(Unit.Value, result);

        }

        public void Dispose()
        {
            
        }
    }
}
