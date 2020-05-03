using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Catalog.API.Domain.Models.Entities
{
    public abstract class BaseEntity 
    {
        public Guid Id { get; protected set; }
        public DateTime CreatedDate { get;  set; }
        public DateTime UpdatedDate { get;  set; }
        public bool IsDeleted { get; set; }


    }
}
