using Dapper;
using DiscountAPI.Models;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System.Threading.Tasks;

namespace DiscountAPI.Repositories
{
    public class DiscountRepositories : IDiscountRepository
    {
        private readonly IConfiguration _configuration;

        public DiscountRepositories(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var conn = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var coupon = await conn.QueryFirstOrDefaultAsync<Coupon>
                        ("select ID, ProductName, Description, Amount from Coupon " +
                        "where ProductName = @ProductName", new { productName = productName });

            if(coupon == null)
            {
                return new Coupon { ProductName = "No Discount", Description = "No Discount Description", Amount = 0 };
            }

            return coupon;
        }
        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var conn = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var createCoupon = await conn.ExecuteAsync
                        ("insert into Coupon (ProductName, Description, Amount) Values (@ProductName, @Description, @Amount)",
                        new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

            if (createCoupon == 0)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var conn = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var updateCoupon = await conn.ExecuteAsync
                        ("Update Coupon set ProductName=@ProductName, Description=@Description, Amount=@Amount where Id = @Id",
                        new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

            if (updateCoupon == 0)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteDiscount(string productName)
        {
            using var conn = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var deleteCoupon = await conn.ExecuteAsync
                        ("Delete from Coupon where ProductName = @ProductName",
                        new { ProductName = productName });

            if (deleteCoupon == 0)
            {
                return false;
            }

            return true;
        }
    }
}
