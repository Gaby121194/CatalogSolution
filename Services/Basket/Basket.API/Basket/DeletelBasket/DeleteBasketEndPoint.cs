

namespace Basket.API.Basket.DeletelBasket
{
    public class DeleteBasketEndPoint
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/basket/{userName}", async (string userName, ISender sender) =>
            {

                var result = await sender.Send(new DeleteBasketCommand(userName));

                var response = result;

                return Results.Ok(response);
            })
            .WithName("Delete basket")
            .Produces<bool>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete basket")
            .WithDescription("Delete basket");
        }
    }
}
