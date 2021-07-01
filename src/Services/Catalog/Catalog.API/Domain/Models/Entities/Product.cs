using System.Collections.Generic;

namespace Catalog.API.Domain.Models.Entities
{
    public class Product : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public ICollection<Category> Categories { get; set; }

        public ICollection<Review> Reviews { get; set; }

    }
}
