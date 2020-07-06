using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Application.Messages.Commands.Catalog
{
    public class All
    {

        public class Query:IRequest<List<Product>>{}

        public class Handler : IRequestHandler<Query, List<Product>>
        {
            private readonly CatalogDataContext dataContext;

            public Handler(CatalogDataContext dataContext)
            {
                this.dataContext = dataContext;
            }

            public async Task<List<Product>> Handle(Query request, CancellationToken cancellationToken)
            {
                var products = await dataContext.Products.ToListAsync();
                return products;
            }
        }


    }
}
