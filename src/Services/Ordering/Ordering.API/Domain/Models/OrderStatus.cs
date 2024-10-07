namespace Ordering.API.Domain.Models
{
    public enum OrderStatus
    {
   
        PaymentPending,
        Accepted,
        Shipped,
        Delevered,
        Returned,
        Canceled
        
    }
}