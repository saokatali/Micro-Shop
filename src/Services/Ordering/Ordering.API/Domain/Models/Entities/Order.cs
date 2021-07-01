using System;
using System.Collections.Generic;
using Ordering.API.Application.Dtos;

namespace Ordering.API.Domain.Models.Entities
{
    public class Order : BaseEntity
    {
        public long OrderId { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public double Total { get; set; }
        public DateTime DeleveredBy { get; set; }
        public ShippingDto Shipping { get; set; }

        public IList<OrderItem> Items { get; set; }

    }
}
