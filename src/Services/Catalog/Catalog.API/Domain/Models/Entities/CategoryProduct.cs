using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Domain.Models.Entities
{
    public class CategoryProduct
    {
        public Guid CategoryId { get; set; }
        public Category Category { get; set; }

        public Guid ProductId { get; set; }
        public Product Product { get; set; }

    }
}
