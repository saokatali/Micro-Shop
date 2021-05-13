using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Common.Web.Middleware;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Infrastructure;

namespace Ordering.API.Application.Messages.Commands.Orders
{
    public class Delete
    {
        public class Command:IRequest<Unit>
        {
            public long OrderId { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        { 
            private readonly DataContext dataContext;

            public Handler(DataContext dataContext)
            {
                this.dataContext = dataContext;
            }
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = await dataContext.Orders.Include(e => e.Items).SingleOrDefaultAsync(e => e.OrderId == request.OrderId);
                if(order==null)
                {
                    throw new NotFoundException($"Order with id {request.OrderId} not found");
                }

                dataContext.Orders.Remove(order);
                await dataContext.SaveChangesAsync();
                return Unit.Value;
            }

        }
    }
}
