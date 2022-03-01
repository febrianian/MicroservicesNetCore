using DiscountAPI.Models;
using System.Threading.Tasks;

namespace DiscountAPI.Repositories
{
    public interface IDiscountRepository
    {
        Task<Coupon> GetDiscount(string productName);
        Task<bool> CreateDiscount(Coupon coupon);
        Task<bool> UpdateCreateDiscount(Coupon coupon);
        Task<bool> DeleteDiscount(string productName);
    }
}
