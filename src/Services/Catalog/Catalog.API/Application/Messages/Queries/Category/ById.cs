using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Infrastructure;
using Common.Web.Middleware;
using MediatR;

namespace Catalog.API.Application.Messages.Queries
{
    public class ById
    {
        public class Query : IRequest<CategoryDto>
        {
            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, CategoryDto>
        {
            private readonly CatalogDataContext dataContext;

            private readonly IMapper mapper;

            public Handler(CatalogDataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<CategoryDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await dataContext.Categories.FindAsync(request.Id);
                if (category == null)
                {

                    throw new NotFoundException($"The Category with id {request.Id} not found");
                }
                return mapper.Map<CategoryDto>(category);
            }
        }
    }
}
