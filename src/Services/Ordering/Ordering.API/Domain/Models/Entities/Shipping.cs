﻿using System;

namespace Ordering.API.Domain.Models.Entities
{
    public class Shipping
    {
        public Guid ShippingId { get; set; }
        public string AwbNumber { get; set; }
        public string CourierName { get; set; }
        public DateTime ShiipedDate { get; set; }

    }
}
