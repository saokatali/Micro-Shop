using System;
using System.Collections.Generic;

namespace Catalog.API.Common.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Vendor { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public List<Guid> CaregoryIds { get; set; }
    }
}
