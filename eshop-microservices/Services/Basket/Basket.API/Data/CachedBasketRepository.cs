
using System.Text.Json;
using Microsoft.Extensions.Caching.Distributed;

namespace Basket.API.Data
{
    public class CachedBasketRepository(IBasketRepository basketRepository, IDistributedCache distributedCache) : IBasketRepository
    {
        public async Task<bool> DeleteBasket(string UserName, CancellationToken cancellationToken = default)
        {
            await basketRepository.DeleteBasket(UserName, cancellationToken);
            await distributedCache.RemoveAsync(UserName);
            return true;
        }

        public async Task<ShoppingCart> GetBasket(string UserName, CancellationToken cancellationToken = default)
        {
            var cachedBasket = await distributedCache.GetStringAsync(UserName, cancellationToken);
            if (!string.IsNullOrEmpty(cachedBasket)) { 
                return JsonSerializer.Deserialize<ShoppingCart>(cachedBasket)!;
            }
            var basket = await basketRepository.GetBasket(UserName, cancellationToken);
            Console.WriteLine(basket);
            await distributedCache.SetStringAsync(UserName, JsonSerializer.Serialize(basket), cancellationToken);
            return basket;
        }

        public async Task<ShoppingCart> StoreBasket(ShoppingCart basket, CancellationToken cancellationToken = default)
        {
            await basketRepository.StoreBasket(basket, cancellationToken);
            await distributedCache.SetStringAsync(basket.UserName, JsonSerializer.Serialize(basket),cancellationToken);
            return basket;
        }
    }
}
