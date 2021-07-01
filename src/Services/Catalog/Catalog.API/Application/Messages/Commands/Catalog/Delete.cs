using System;
using System.Threading;
using System.Threading.Tasks;
using Catalog.API.Infrastructure;
using Common.Web.Middleware;
using MediatR;

namespace Catalog.API.Application.Messages.Commands.Catalog
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
                var product = dataContext.Products.Find(request.Id);

                if (product == null)
                {

                    throw new NotFoundException($"The product with id {request.Id} not found");
                }

                product.UpdatedDate = DateTime.UtcNow;
                product.IsDeleted = true;
                await dataContext.SaveChangesAsync();

                return new Unit();


            }
        }
    }
}
