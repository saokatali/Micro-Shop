using System;
using System.Threading.Tasks;
using Catalog.API.Application.Dto;
using Catalog.API.Application.Messages.Commands.Comment;
using Catalog.API.Application.Messages.Queries.Review;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace Catalog.API.Hubs
{
    public class ReviewHub : Hub
    {
        private readonly IMediator mediator;

        public ReviewHub(IMediator mediator)
        {
            this.mediator = mediator;
        }


        public override Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var productId = httpContext.Request.Query["productId"];
            Groups.AddToGroupAsync(Context.ConnectionId, productId[0]);
            Clients.Caller.SendAsync("LoadReviews", mediator.Send(new AllByProduct.Query { ProductId = Guid.Parse(productId) }));
            return base.OnConnectedAsync();
        }

        public async Task Add(ReviewDto review)
        {
            await mediator.Send(new Add.Command { Review = review });
            await Clients.Group(review.ProductId.ToString()).SendAsync("GetReview", review);

        }



    }
}
