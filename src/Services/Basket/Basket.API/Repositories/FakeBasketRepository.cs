using Basket.API.Entities;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Repositories
{
    public class FakeBasketRepository : IBasketRepository
    {
        private static IDictionary<string, string> Repository = new Dictionary<string, string>();

        public async Task DeleteBasket(string username)
        {
            Repository.Remove(username);
        }

        public async Task<ShoppingCart> GetBasket(string username)
        {
            if(Repository.TryGetValue(username, out string basket))
            {
                return JsonConvert.DeserializeObject<ShoppingCart>(basket);
            }

            return null;      
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            var basketDb = await GetBasket(basket.UserName);

            if(basketDb is not null) //update action
            {
                Repository.Remove(basket.UserName);
            }

            Repository.Add(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }
    }
}
