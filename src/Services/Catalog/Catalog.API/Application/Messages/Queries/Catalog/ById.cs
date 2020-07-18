using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Application.Messages.Queries.Catalog
{
    public class ById
    {
        public class Query : IRequest<Product>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Product>
        {
            private readonly CatalogDataContext dataContext;

            public Handler(CatalogDataContext dataContext)
            {
                this.dataContext = dataContext;
            }

            public async Task<Product> Handle(Query request, CancellationToken cancellationToken)
            {
                return await dataContext.Products.FindAsync(request.Id);
            }
        }
    }
}
