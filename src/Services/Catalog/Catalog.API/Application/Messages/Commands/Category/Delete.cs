using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Infrastructure;
using Common.Web.Middleware;
using MediatR;

namespace Catalog.API.Application.Messages.Commands
{
    public class Delete
    {
        public class Command : IRequest
        {

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
                category.IsDeleted = true;
                await dataContext.SaveChangesAsync();

                return new Unit();


            }
        }
    }
}
