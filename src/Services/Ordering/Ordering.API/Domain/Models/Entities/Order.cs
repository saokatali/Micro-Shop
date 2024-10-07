using Ordering.API.Application.Dtos;

namespace Ordering.API.Domain.Models.Entities
{
    public class Order : EntityBase
    {
        public new long Id { get; set; }
        public Guid CustomerId { get; set; }
        public OrderStatus Status { get; set; }
        public string ShippingAddress { get; set; }
        public string BillingAddress { get; set; }
        public double Total { get; set; }
        public DateTime DeleveredBy { get; set; }
        public Shipping Shipping { get; set; }

        public IList<OrderItem> Items { get; set; }
    }
}