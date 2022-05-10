using Discount.Grpc.Protos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Services
{
    public class DiscountGrpcService : IDiscountService
    {
        private readonly DiscountProtoService.DiscountProtoServiceClient _protoService;

        public DiscountGrpcService(DiscountProtoService.DiscountProtoServiceClient service)
        {
            _protoService = service;
        }

        public async Task<CouponModel> GetDiscount(string productName)
        {
            var discountRequest = new GetDiscountRequest
            {
                ProductName = productName
            };

            return await _protoService.GetDiscountAsync(discountRequest);
        }
    }
}
