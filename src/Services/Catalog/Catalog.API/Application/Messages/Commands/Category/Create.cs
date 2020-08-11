using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Catalog.API.Core.Dto;
using Catalog.API.Domain.Models.Entities;
using Catalog.API.Infrastructure;
using MediatR;

namespace Catalog.API.Application.Messages.Commands.Category
{
    public class Create
    {
        public class Command:IRequest
        {
            public CategoryDto Category { get; set; }


        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly CatalogDataContext dataContext;

            private readonly IMapper mapper;

            public Handler(CatalogDataContext dataContext, IMapper mapper)
            {
                this.dataContext = dataContext;
                this.mapper = mapper;
            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
               // var category = mapper.Map<Category>(request.Category);
                var category = new Domain.Models.Entities.Category { Name = request.Category.Name };
                category.Id = new Guid();
                category.CreatedDate = DateTime.UtcNow;
                category.UpdatedDate = DateTime.UtcNow;
                category.IsDeleted = false;

                dataContext.Add(category);
                await dataContext.SaveChangesAsync();

                return new Unit();

                
            }
        }

    }
}
