using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Catalog.API.Application.Messages.Queries
{
    public class All
    {
        public class Query : IRequest<List<CategoryDto>> { }

        public class Handler : IRequestHandler<Query, List<CategoryDto>>
        {
            private readonly CatalogDataContext dataContext;

            private readonly IMapper mapper;

            public Handler(CatalogDataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<List<CategoryDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var categories = await dataContext.Categories.ToListAsync();
                return mapper.Map<List<CategoryDto>>(categories);
            }
        }
    }
}
