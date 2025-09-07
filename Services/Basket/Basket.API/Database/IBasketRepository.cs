namespace Basket.API.Database
{
    public interface IBasketRepository
    {
        Task<bool> DeleteBasket(string userName);
        Task<ShoppingCart> GetBasket(string userName);
        Task<string> StoreBasket(ShoppingCart basket);
    }
}