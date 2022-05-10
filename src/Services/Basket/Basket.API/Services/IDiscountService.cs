using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Services
{
    public interface IDiscountService
    {
        Task<CouponModel> GetDiscount(string productName); 
    }
}
