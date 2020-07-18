using System;

namespace Catalog.API.Domain.Models.Entities
{
    public class Review : BaseEntity
    {
        public Guid Reviewer { get; set; }

        public string ReviewerName { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }

        public Product Product { get; set; }
    }
}
