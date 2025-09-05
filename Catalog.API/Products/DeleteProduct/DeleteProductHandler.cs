

using Catalog.API.Products.CreateProduct;
using FluentValidation;

namespace Catalog.API.Products.DeleteProduct
{

    public record DeleteProductCommand(Guid Id) 
        : ICommand<DeleteProductResult>;

    public record DeleteProductResult(bool IsSucced);

    public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("ID is required");
        }
    }
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
