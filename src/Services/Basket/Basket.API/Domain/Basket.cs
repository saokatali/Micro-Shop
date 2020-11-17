using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Domain
{
    public class Basket
    {
        public List<BasketItem> Items { get; set; }

        public decimal Price
        {
            get
            {
                return Items.Sum(e => e.Price * e.Quantity);
            }
        }
    }
}
