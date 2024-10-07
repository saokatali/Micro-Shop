namespace Ordering.API.Domain.Models.Entities
{
    public class Shipping:EntityBase
    {

        public string AwbNumber { get; set; }
        public string CourierName { get; set; }
        public DateTime ShiipedDate { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
    }
}