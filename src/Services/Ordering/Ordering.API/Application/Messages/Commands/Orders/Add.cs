using AutoMapper;
using MediatR;
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
            private readonly IHttpClientFactory httpClientFactory;

            public Handler(DataContext dbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor, IHttpClientFactory httpClientFactory)
            {
                this.mapper = mapper;
                this.dbContext = dbContext;
                this.httpContextAccessor = httpContextAccessor;
                this.httpClientFactory = httpClientFactory;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var order = mapper.Map<Order>(request.Order);

                //Call Product service to confirm the Inventory

                HttpClient client = httpClientFactory.CreateClient("ProductService");

                dbContext.Orders.Add(order);
                await dbContext.SaveChangesAsync();
                return Unit.Value;
            }
        }
    }
}