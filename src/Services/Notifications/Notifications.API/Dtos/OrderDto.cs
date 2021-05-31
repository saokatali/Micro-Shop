using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Notifications.API.Dtos
{
    public class OrderDto
    {
        public Guid CustomerId { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public double Total { get; set; }
        public DateTime DeleveredBy { get; set; }
        public IList<OrderItemDto> Items { get; set; }
    }
}
