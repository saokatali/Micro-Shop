﻿using System;

namespace Catalog.API.Application.Dto
{
    public class ReviewDto
    {
        public Guid Reviewer { get; set; }

        public string ReviewerName { get; set; }

        public string Comment { get; set; }

        public double Rating { get; set; }

        public Guid ProductId { get; set; }
    }
}
