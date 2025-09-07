

namespace Basket.API.Basket.StoreBasket
{
    public class StoreBasketEndPoint : ICarterModule
    {
        public record StoreBasketRequest(ShoppingCart Cart);
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var commando = request.Adapt<StoreBasketCommand>();

                var result = await sender.Send(commando);

                var response = result;

                return Results.Created($"/products/", true);
            })
            .WithName("Store basket")
            .Produces<bool>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store basket service")
            .WithDescription("Store basket service");
        }
    }
}
