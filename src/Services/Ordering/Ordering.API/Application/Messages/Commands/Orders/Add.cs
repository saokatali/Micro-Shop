using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Http;
using Ordering.API.Application.Dtos;
using Ordering.API.Domain.Models.Entities;
using Ordering.API.Infrastructure;

namespace Ordering.API.Application.Messages.Commands.Orders
{
    public class Add
    {
        public class Command : IRequest<Unit>
        {
            public OrderDto Order { get; set; }
        }

        public class Handler : IRequestHandler<Command, Unit>
        {
            private readonly IMapper mapper;
            private readonly DataContext dbContext;
            private readonly IHttpContextAccessor httpContextAccessor;

            public Handler(DataContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
                this.httpContextAccessor = httpContextAccessor;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = mapper.Map<Order>(request.Order);
                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}
