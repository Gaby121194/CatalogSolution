

namespace Catalog.API.Products.DeleteProduct
{

    internal record DeleteProductResponse(bool IsSucced);
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/product/{id}", async (Guid id, ISender sender) =>
            {

                var result = await sender.Send(new DeleteProductCommand(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);
            })
            .WithName("Delete product")
            .Produces<DeleteProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Delete prudcto")
            .WithDescription("Delete products");        }
    }
}
