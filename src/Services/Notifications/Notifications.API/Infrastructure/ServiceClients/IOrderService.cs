using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Notifications.API.Dtos;

namespace Notifications.API.Infrastructure.ServiceClients
{
    public interface IOrderService
    {
       Task<OrderDto> GetDetails(long orderId);

    }
}
