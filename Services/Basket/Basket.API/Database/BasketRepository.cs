
namespace Basket.API.Database
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDocumentSession _session;
        public BasketRepository(IDocumentSession session)
        {
            _session = session;
        }
        public async Task<bool> DeleteBasket(string userName)
        {
            var basket = await _session.LoadAsync<ShoppingCart>(userName);
            if(basket is null) throw new NotFoundException(userName);
            _session.Delete<ShoppingCart>(userName);
            await _session.SaveChangesAsync();
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _session.LoadAsync<ShoppingCart>(userName);
            return basket ?? throw new NotFoundException(userName);
        }

        public async Task<string> StoreBasket(ShoppingCart basket)
        {
            
            _session.Store(basket);
            await _session.SaveChangesAsync();
            return basket.UserName;
        }
    }
}
