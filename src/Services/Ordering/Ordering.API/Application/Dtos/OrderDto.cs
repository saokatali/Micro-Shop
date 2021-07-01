using System;
using System.Collections.Generic;
using Ordering.API.Domain.Models;
using Ordering.API.Domain.Models.Entities;

namespace Ordering.API.Application.Dtos
{
    public class OrderDto
    {
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public double Total { get; set; }
        public DateTime DeleveredBy { get; set; }
        public Shipping Shipping { get; set; }

        public IList<OrderItemDto> Items { get; set; }
    }
}
