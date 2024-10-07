namespace Ordering.API.Domain.Events
{
    public class OrderCreated:EventBase
    {
        public  long OrderId { get; set; }
        public Guid CustomerId { get; set; }
    }
}