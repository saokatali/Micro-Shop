using AutoMapper;
using Catalog.API.Application.Dto;
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

namespace Catalog.API.Application.Messages.Queries.Review
{
    public class AllByProduct
    {

        public class Query : IRequest<List<ReviewDto>> 
        {
            public Guid  ProductId { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<ReviewDto>>
        {
            private readonly CatalogDataContext dataContext;

            private readonly IMapper mapper;

            public Handler(CatalogDataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<List<ReviewDto>> Handle(Query request, CancellationToken cancellationToken)
            {
                var product = await dataContext.Products.Include(e => e.Reviews).Where(e => e.Id == request.ProductId).FirstOrDefaultAsync();

                return product.Reviews.Select(e => mapper.Map<ReviewDto>(e)).ToList();
            }
        }


    }
}
