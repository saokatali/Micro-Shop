using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Application.Messages.Queries.Category
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
