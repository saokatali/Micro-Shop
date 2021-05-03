using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Application.Messages.Queries.Catalog
{
    public class All
    {

        public class Query : IRequest<List<ProductDto>> { }

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
                var products = await dataContext.Products.Include(e=>e.Categories).ToListAsync();

                var data = products.Select(p => {
                    var productDto = mapper.Map<ProductDto>(p);
                    productDto.CaregoryIds = p.Categories.Select(c => c.CategoryId).ToList();
                    return productDto;
                    }).ToList();

              
                return data;
            }
        }


    }
}
