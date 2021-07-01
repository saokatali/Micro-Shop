using System;

namespace Ordering.API.Application.Dtos
{
    public class ShippingDto
    {
        public Guid ShippingId { get; set; }
        public string AwbNumber { get; set; }
        public string CourierName { get; set; }
        public DateTime ShiipedDate { get; set; }
    }
}
