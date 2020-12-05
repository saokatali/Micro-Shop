using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Infrastructure;
using MediatR;

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

            public Handler(ICacheContext cacheContext)
            {
                this.cacheContext = cacheContext;
            }


            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var basket = await cacheContext.GetAsync<Domain.Basket>("basket");
                if (basket == null)
                {
                    basket = new Domain.Basket();
                }



                basket.Items.Remove(basket.Items.Find(e=>e.ProductId == request.ProductId));

                var result = await cacheContext.SetAsync("basket", basket);
                if (result) return Unit.Value;
                throw new Exception("Problem saving data");

            }
        }
    }
}
