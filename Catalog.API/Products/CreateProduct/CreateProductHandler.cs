

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name, string Description, decimal Price, string ImageFile, List<string> Category) 
        : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    public class GetProductByIdHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product entity from command object
            var product = new Product()
            {
                Name = command.Name,
                Description = command.Description,
                Price = command.Price,
                ImageFile = command.ImageFile,
                Category = command.Category,
            };
            //save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            //return CreatePoductResult result
            return new CreateProductResult(product.Id); 
        }
    }
}
