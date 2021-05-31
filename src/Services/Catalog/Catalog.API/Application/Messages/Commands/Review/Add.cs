using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Application.Dto;
using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using MediatR;

namespace Catalog.API.Application.Messages.Commands.Comment
{
    public class Add
    {
        public class Command : IRequest<Unit>
        {
            public ReviewDto Review { get; set; }


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

                Review review = mapper.Map<Review>(request.Review);

                var product = await dataContext.Products.FindAsync(request.Review.ProductId);

                product.Reviews.Add(review);

                return new Unit();


            }
        }

    }
}
