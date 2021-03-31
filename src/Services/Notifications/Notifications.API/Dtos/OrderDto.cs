using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.API.Dtos
{
    public class OrderDto
    {
        public Guid CorrelationId { get; set; }

        public Guid OrderId { get; set; }

        public Guid UserId { get; set; }
    }
}
