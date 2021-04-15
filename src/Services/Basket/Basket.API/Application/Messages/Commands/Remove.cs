using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Basket.API.Application.Messages.Commands
{
    public class Remove
    {
        public class Command : IRequest
        {
            public Guid ProductId { get; set; }


        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly ICacheContext cacheContext;
            HttpContext httpContext;

            public Handler(ICacheContext cacheContext, IHttpContextAccessor httpContextAccessor)
            {
                this.cacheContext = cacheContext;
                this.httpContext = httpContextAccessor.HttpContext;
            }


            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var userId = httpContext.Request.Headers["userId"];
                var basket = await cacheContext.GetAsync<Domain.Basket>(userId);
                if (basket == null)
                {
                    basket = new Domain.Basket();
                }



                basket.Items.Remove(basket.Items.Find(e=>e.ProductId == request.ProductId));

                var result = await cacheContext.SetAsync(userId, basket);
                if (result) return Unit.Value;
                throw new Exception("Problem saving data");

            }
        }
    }
}
