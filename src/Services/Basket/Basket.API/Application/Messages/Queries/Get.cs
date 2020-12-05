using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Domain;
using Basket.API.Infrastructure;
using MediatR;

namespace Basket.API.Application.Messages.Queries
{
    public class Get
    {
        public class Query:IRequest<Domain.Basket>
        {


        }

        public class Handler : IRequestHandler<Query, Domain.Basket>
        {
            private readonly ICacheContext cacheContext;

            public Handler(ICacheContext cacheContext)
            {
                this.cacheContext = cacheContext;
            }
            public async Task<Basket.API.Domain.Basket> Handle(Query request, CancellationToken cancellationToken)
            {
                var basket = await cacheContext.GetAsync<Domain.Basket>("basket");
                if (basket == null)
                {
                    basket = new Domain.Basket();
                }

                return basket;

            }
        }

    }
}
