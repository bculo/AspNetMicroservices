using Discount.Data.Contracts;
using Discount.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Discount.Data.Repositories
{
    public class FakeDiscountRepository : IDiscountRepository
    {
        private static List<Coupon> Coupons = new List<Coupon>
        {
            new Coupon
            {
                Id = 1,
                Amount = 150,
                ProductName = "IPhone X",
                Description = "IPhone Discount"
            },
            new Coupon
            {
                Id = 2,
                Amount = 100,
                ProductName = "Samsung 10",
                Description = "Samsung Discount"
            },
        };

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            Coupons.Add(coupon);

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            var deleted = Coupons.RemoveAll(item => item.ProductName == productName);

            return deleted > 0;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            return Coupons.Find(t => t.ProductName == productName);
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            var itemToUpdate = Coupons.Find(item => item.Id == coupon.Id);

            if (itemToUpdate is null)
            {
                return false;
            }

            itemToUpdate.Description = coupon.Description;
            itemToUpdate.ProductName = coupon.ProductName;
            itemToUpdate.Amount = coupon.Amount;

            return true;
        }
    }
}
