using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.API.Dtos
{
    public class OrderCreatedData
    {
        public Guid CorrelationId { get; set; }

        public long OrderId { get; set; }

        public Guid UserId { get; set; }
    }
}
