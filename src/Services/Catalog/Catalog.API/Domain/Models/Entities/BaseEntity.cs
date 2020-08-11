﻿using System;

namespace Catalog.API.Domain.Models.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get;  set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }


    }
}