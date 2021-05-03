using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Infrastructure;
using Common.Web.Middleware;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Catalog.API.Domain.Models.Entities;

namespace Catalog.API.Application.Messages.Commands.Catalog
{
    public class Update
    {
        public class Command : IRequest
        {
            public ProductDto Product { get; set; }
            public Guid Id { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly CatalogDataContext dataContext;

            private readonly IMapper mapper;

            public Handler(CatalogDataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var product = await dataContext.Products.Include(p => p.Categories).SingleOrDefaultAsync(p => p.Id == request.Id);

                if (product == null)
                {

                    throw new NotFoundException($"The product with id {request.Id} not found");
                }

                product.UpdatedDate = DateTime.UtcNow;
                product.Name = request.Product.Name;
                product.Description = request.Product.Description;
                product.Quantity = request.Product.Quantity;
                product.Price = request.Product.Price;
                product.Vendor = request.Product.Vendor;

                product.Categories.Clear();

                foreach (var categoryId in request.Product.CaregoryIds)
                {
                    product.Categories.Add(new CategoryProduct { ProductId = product.Id, CategoryId = categoryId });

                }

                await dataContext.SaveChangesAsync();

                return new Unit();


            }


        }

    }
}
