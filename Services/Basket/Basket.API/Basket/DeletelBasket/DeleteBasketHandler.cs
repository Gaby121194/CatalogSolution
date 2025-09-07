


using Basket.API.Database;

namespace Basket.API.Basket.DeletelBasket
{
    public record DeleteBasketCommand(string UserName) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSucced);
    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {
        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            var basketDeleted = await repository.DeleteBasket(request.UserName);

            return new DeleteBasketResult(basketDeleted);
        }
    }
}
