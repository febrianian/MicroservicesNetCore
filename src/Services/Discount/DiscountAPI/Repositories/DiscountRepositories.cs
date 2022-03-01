using DiscountAPI.Models;
using System.Threading.Tasks;

namespace DiscountAPI.Repositories
{
    public class DiscountRepositories : IDiscountRepository
    {

        public DiscountRepositories()
        {

        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            throw new System.NotImplementedException();
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            throw new System.NotImplementedException();
        }
        public async Task<bool> UpdateCreateDiscount(Coupon coupon)
        {
            throw new System.NotImplementedException();
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            throw new System.NotImplementedException();
        }
    }
}
