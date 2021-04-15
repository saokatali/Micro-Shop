using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Domain;
using Basket.API.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;

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
            HttpContext httpContext;

            public Handler(ICacheContext cacheContext, IHttpContextAccessor httpContextAccessor)
            {
                this.cacheContext = cacheContext;
                this.httpContext = httpContextAccessor.HttpContext;
            }
            public async Task<Basket.API.Domain.Basket> Handle(Query request, CancellationToken cancellationToken)
            {
                var userId = httpContext.Request.Headers["claims_userId"];
                var basket = await cacheContext.GetAsync<Domain.Basket>(userId);
                if (basket == null)
                {
                    basket = new Domain.Basket();
                }

                return basket;

            }
        }

    }
}
