

namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id,string Name, string Description, decimal Price, string ImageFile, List<string> Category) 
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSucced);
    public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            //create Product entity from command object
            var product = await session.LoadAsync<Product>(command.Id, cancellationToken);
            if (product == null) {
                throw new ProductNotFoundException();
            }
            product.Name = command.Name;
            product.Description = command.Description;
            product.Price = command.Price;
            product.ImageFile = command.ImageFile;
            product.Category = command.Category;

            //save to database
            session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            //return CreatePoductResult result
            return new UpdateProductResult(true); 
        }
    }
}
