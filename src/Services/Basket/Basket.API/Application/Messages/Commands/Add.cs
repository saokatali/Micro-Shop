using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Basket.API.Domain;
using Basket.API.Infrastructure;
using MediatR;

namespace Basket.API.Application.Messages.Commands
{
    public static class Add
    {
        public class Command : IRequest
        {
            public BasketItem Item { get; set; }


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
                if(basket == null)
                {
                    basket = new Domain.Basket();
                }

                basket.Items.Add(request.Item);

               var  result = await cacheContext.SetAsync("basket", basket);            
                if(result) return Unit.Value;
                throw new Exception("Problem saving data");
                
            }
        }
    }
}
