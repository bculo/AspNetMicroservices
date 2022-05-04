using Dapper;
using Discount.Grpc.Entities;
using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace Discount.Grpc.Repositories
{
    public class DiscountRepository : IDiscountRepository
    {
        private readonly IConfiguration _config;

        private IDbConnection Connection 
        {
            get
            {
                return new NpgsqlConnection(_config.GetValue<string>("DatabaseSettings:ConnectionString"));
            }
        }

        public DiscountRepository(IConfiguration config)
        {
            _config = config;
        }

        public async Task<bool> CreateDiscount(Coupon coupon)
        {
            using var queryConnection = Connection;

            var affected = await queryConnection.ExecuteAsync(
                "INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@Name, @Description, @Amount);",
                new
                {
                    Name = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> DeleteDiscount(string productName)
        {
            using var queryConnection = Connection;

            var affected = await queryConnection.ExecuteAsync(
                "DELETE FROM coupon WHERE ProductName = @Name",
                new
                {
                    Name = productName,
                });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }

        public async Task<Coupon> GetDiscount(string productName)
        {
            using var queryConnection = Connection;

            return await queryConnection.QueryFirstOrDefaultAsync<Coupon>("SELECT * FROM COUPON WHERE ProductName = @ProductName", new { ProductName = productName});
        }

        public async Task<bool> UpdateDiscount(Coupon coupon)
        {
            using var queryConnection = Connection;

            var affected = await queryConnection.ExecuteAsync(
                "UPDATE coupon SET amount = @Amount, description = @Description, productname = @Name WHERE id = @Id",
                new
                {
                    Id = coupon.Id,
                    Name = coupon.ProductName,
                    Description = coupon.Description,
                    Amount = coupon.Amount,
                });

            if (affected == 0)
            {
                return false;
            }

            return true;
        }
    }
}
