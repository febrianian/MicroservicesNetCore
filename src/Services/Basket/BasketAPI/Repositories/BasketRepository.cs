using BasketAPI.Model;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using System.Threading.Tasks;

namespace BasketAPI.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IDistributedCache _redisCahce;
        public BasketRepository(IDistributedCache redisCahce)
        {
            _redisCahce = redisCahce;
        }        

        public async Task<ShoppingCart> GetBasket(string userName)
        {
            var basket = await _redisCahce.GetStringAsync(userName);

            if (string.IsNullOrEmpty(basket))
                return null;

            return JsonConvert.DeserializeObject<ShoppingCart>(basket);
        }

        public async Task<ShoppingCart> UpdateBasket(ShoppingCart basket)
        {
            await _redisCahce.SetStringAsync(basket.UserName, JsonConvert.SerializeObject(basket));

            return await GetBasket(basket.UserName);
        }
        public async Task DeleteBasket(string userName)
        {
            await _redisCahce.RemoveAsync(userName);
        }
    }
}
