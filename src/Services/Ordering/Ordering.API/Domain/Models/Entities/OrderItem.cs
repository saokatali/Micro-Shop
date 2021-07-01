using System;

namespace Ordering.API.Domain.Models.Entities
{
    public class OrderItem
    {
        public long OrderItemId { get; set; }
        public Order Order { get; set; }
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

    }
}
