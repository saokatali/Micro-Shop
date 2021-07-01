using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Infrastructure;
using Common.Web.Middleware;
using MediatR;


namespace Catalog.API.Application.Messages.Queries.Catalog
{
    public class ById
    {
        public class Query : IRequest<ProductDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, ProductDto>
        {
            private readonly CatalogDataContext dataContext;

            private readonly IMapper mapper;

            public Handler(CatalogDataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<ProductDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await dataContext.Products.FindAsync(request.Id);
                if (product == null)
                {

                    throw new NotFoundException($"The Product with id {request.Id} not found");
                }
                return mapper.Map<ProductDto>(product);
            }
        }
    }
}
