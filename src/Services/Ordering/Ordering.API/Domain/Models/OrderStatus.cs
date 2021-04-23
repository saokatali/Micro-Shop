using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Domain.Models
{
    public enum OrderStatus
    {
        PaymentPending,
        Shipped,
        Delevered,
        Returned

    }
}
