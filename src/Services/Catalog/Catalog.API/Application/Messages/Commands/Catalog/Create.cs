using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Application.Messages.Commands.Catalog
{
    public class Create
    {
        public class Command : IRequest
        {
            public ProductDto Product { get; set; }


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
                var newProduct = request.Product;
                var product = new Product { Name = newProduct.Name, Description = newProduct.Description, Price = newProduct.Price, Quantity = newProduct.Quantity, Vendor = newProduct.Vendor };

                product.Id = newProduct.Id != default ? newProduct.Id : product.Id;
                product.Categories = new List<CategoryProduct>();
                foreach (var categoryId in newProduct.CaregoryIds)
                {
                    product.Categories.Add(new CategoryProduct { ProductId = product.Id, CategoryId = categoryId });

                }
            
                dataContext.Add(product);
                var success = await dataContext.SaveChangesAsync() > 0;
                if (success)
                {
                    return Unit.Value;
                }

                throw new Exception("Problem saving data");

            }
        }


    }
}
