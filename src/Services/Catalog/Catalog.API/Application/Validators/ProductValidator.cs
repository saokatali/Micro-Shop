using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Common.Dto;
using FluentValidation;

namespace Catalog.API.Application.Validators
{
    public class ProductValidator:AbstractValidator<ProductDto>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Name).NotEmpty();
            RuleFor(p => p.Description).NotEmpty();
            RuleFor(p => p.Price).NotEmpty();
            RuleFor(p => p.Quantity).NotEmpty();
        }
    }
}
