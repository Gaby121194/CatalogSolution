

namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid Id) 
        : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSucced);
    public class DeleteProductHandler(IDocumentSession session, ILogger<DeleteProductHandler> logger) : ICommandHandler<DeleteProductCommand, DeleteProductResult>
    {
        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            //create Product entity from command object
            session.Delete<Product>(command.Id);
            
            await session.SaveChangesAsync(cancellationToken);
            //return CreatePoductResult result
            return new DeleteProductResult(true); 
        }
    }
}
