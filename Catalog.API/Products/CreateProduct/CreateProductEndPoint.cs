

namespace Catalog.API.Products.CreateProduct
{
    internal record CreateProductRequest(string Name, string Description, decimal Price, string ImageFile, List<string> Category);

    internal record CreateProductResponse(Guid Id);
    public class CreateProductEndPoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender sender) =>
            {
                var commando = request.Adapt<CreateProductCommand>();

                var result = await sender.Send(commando);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("Create product")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Create prudcto")
            .WithDescription("Create products");        }
    }
}
