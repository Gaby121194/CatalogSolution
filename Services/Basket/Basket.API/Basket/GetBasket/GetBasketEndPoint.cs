
namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart cart);
    public class GetBasketEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (ISender sender, string userName ) =>
            {
                var query = new GetBasketQuery(userName);

                var result = await sender.Send(query);

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);
            })
            .WithName("Get cart")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get cart")
            .WithDescription("Get cart");
        }
    }
}
