using Basket.API.Entities;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class BaskRepository : IBasketRepository
    {
        private readonly IDistributedCache _cache;

        public BaskRepository(IDistributedCache cache)
        {
            _cache = cache;
        }

        public async Task DeleteBasket(string username)
        {
            await _cache.RemoveAsync(username);
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            var basket = await _cache.GetStringAsync(username);

            if (string.IsNullOrEmpty(basket))
            {
                return null;
            }

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            var basketJson = JsonConvert.SerializeObject(basket);

            await _cache.SetStringAsync(basket.UserName, basketJson);

            return await GetBasket(basket.UserName);
        }
    }
}
