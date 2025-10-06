using Basket.Core.Entites;
using Basket.Core.Repositories;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Basket.Infrastructure.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private  readonly IDistributedCache _redisCache;

        public BasketRepository(IDistributedCache redisCache)
        {
            _redisCache=redisCache;
        }


        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket=await _redisCache.GetStringAsync(userName);
            if (string.IsNullOrEmpty(basket))
                return null;
            
            return JsonConvert.DeserializeObject<ShoppingCart>(basket);

        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart cart)
        {
            var basket = await _redisCache.GetStringAsync(cart.UserName);
            if(!string.IsNullOrEmpty(basket))
            {
                //login return
                return await GetBasket(cart.UserName);
            }
            await _redisCache.SetStringAsync(cart.UserName, JsonConvert.SerializeObject(cart));
            return await GetBasket(cart.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            var basket =await _redisCache.GetStringAsync(userName);
            if(!string.IsNullOrEmpty(basket))
            {
                await _redisCache.RemoveAsync(userName);
            }
        }
    }
}
