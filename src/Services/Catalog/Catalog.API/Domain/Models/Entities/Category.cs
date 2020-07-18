using System.Collections.Generic;

namespace Catalog.API.Domain.Models.Entities
{
    public class Category : BaseEntity
    {
        public string Name { get; set; }

        public List<CategoryProduct> Products { get; set; }
    }
}
