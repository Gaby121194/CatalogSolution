
using Basket.API.Database;
using Discount.gRPC;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart can not be null");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName is required");
        }
    }
    public class StoreBasketHandler(IBasketRepository repository, DiscountProto.DiscountProtoClient discountService ) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            await ApplyDiscount(command.Cart);
            await repository.StoreBasket(command.Cart);
            return new StoreBasketResult(command.Cart.UserName);
        }

        public async Task ApplyDiscount(ShoppingCart cart)
        {
            foreach (var item in cart.Items)
            {
                var discount = await discountService.GetDiscountAsync(new GetDiscountRequest { ProducName = item.ProductName });
                item.Price -= discount.Amount;
            }
        }
    }
}
