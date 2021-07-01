using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Infrastructure;
using Common.Web.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Application.Messages.Queries.Catalog
{
    public class ByCategory
    {
        public class Query : IRequest<List<ProductDto>>
        {
            public Guid CategoryId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<ProductDto>>
        {
            private readonly CatalogDataContext dataContext;

            private readonly IMapper mapper;

            public Handler(CatalogDataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<List<ProductDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var category = await dataContext.Categories.Include(e => e.Products).FirstOrDefaultAsync(e => e.Id == request.CategoryId);



                if (category == null)
                {

                    throw new NotFoundException($"The Category with id {request.CategoryId} not found");
                }
                return mapper.Map<List<ProductDto>>(category.Products);
            }
        }
    }
}
