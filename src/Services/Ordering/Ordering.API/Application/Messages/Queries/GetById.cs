using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Ordering.API.Application.Dtos;
using Ordering.API.Infrastructure;
using AutoMapper.QueryableExtensions;
using AutoMapper;

namespace Ordering.API.Application.Messages.Queries
{
    public class GetById
    {
        public class Query : IRequest<OrderDto>
        {
            public long orderId { get; set; }

        }


        public class Handler : IRequestHandler<Query, OrderDto>
        {

            private readonly DataContext dataContext;
            private readonly IMapper mapper;

            public Handler(DataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<OrderDto> Handle(Query request, CancellationToken cancellationToken)
            {
                var order = await dataContext.Orders.Include(o => o.Items).FirstOrDefaultAsync(o => o.OrderId == request.orderId);

                return mapper.Map<OrderDto>(order);

            }
        }

    }
}
