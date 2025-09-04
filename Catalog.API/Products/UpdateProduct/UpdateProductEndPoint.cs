

namespace Catalog.API.Products.UpdateProduct
{
    internal record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, string ImageFile, List<string> Category);

    internal record UpdateProductResponse(bool IsSucced);
    public class DeleteProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPut("/product", async (UpdateProductRequest request, ISender sender) =>
            {
                var commando = request.Adapt<UpdateProductCommand>();

                var result = await sender.Send(commando);

                var response = result.Adapt<UpdateProductResponse>();

                return Results.Ok(response);
            })
            .WithName("Update product")
            .Produces<UpdateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Update prudcto")
            .WithDescription("Update products");        }
    }
}
