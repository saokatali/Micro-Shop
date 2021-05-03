using AutoMapper;
using Catalog.API.Common.Dto;
using Catalog.API.Infrastructure;
using Common.Web.Middleware;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Catalog.API.Application.Messages.Commands.Category
{
    public class Update
    {
        public class Command : IRequest
        {
            public CategoryDto Category { get; set; }
            public Guid Id { get; set; }

        }

        public class Handler : IRequestHandler<Command>
        {
            private readonly CatalogDataContext dataContext;

        

            public Handler(CatalogDataContext dataContext)
            {
                this.dataContext = dataContext;

            }

            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                // var category = mapper.Map<Category>(request.Category);
                var category = dataContext.Categories.Find(request.Id);

                if (category == null)
                {

                    throw new NotFoundException($"The category with id {request.Id} not found");
                }

                category.UpdatedDate = DateTime.UtcNow;
                category.Name = request.Category.Name;             
                await dataContext.SaveChangesAsync();

                return new Unit();


            }
        }

    }
}
