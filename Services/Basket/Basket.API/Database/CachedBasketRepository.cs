
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Basket.API.Database
{
    public class CachedBasketRepository(IBasketRepository repository, IDistributedCache cache ) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string userName)
        {
            await repository.DeleteBasket(userName);
            await cache.RemoveAsync(userName);
            return true;
        }


        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var cachedBasket = await cache.GetStringAsync(userName);
            if(!string.IsNullOrEmpty(cachedBasket))
            {
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket);
            }
            var basket = await repository.GetBasket(userName);
            await cache.SetAsync(userName, System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(basket));
            return basket;
        }

        public async Task<string> StoreBasket(ShoppingCart basket)
        {
            await repository.StoreBasket(basket);

            await cache.SetAsync(basket.UserName, System.Text.Json.JsonSerializer.SerializeToUtf8Bytes(basket));
            
            return basket.UserName;
        }


    }
}
