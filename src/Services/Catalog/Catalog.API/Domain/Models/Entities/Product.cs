using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Domain.Models.Entities
{
    public class Product: BaseEntity
    {
        public string Name { get;  set; }
        public string Description { get;  set; }
        public string Vendor { get;  set; }
        public decimal Price { get;  set; }
        public int Quantity { get;  set; }

        public List<CategoryProduct> Categories { get; set; }

        public List<Review> Reviews;

    }
}
