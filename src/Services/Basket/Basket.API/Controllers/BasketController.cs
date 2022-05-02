using Basket.API.Entities;
using Basket.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Basket.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class BasketController : ControllerBase
    {
        private readonly IBasketRepository _repository;

        public BasketController(IBasketRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);

            if(basket is null)
            {
                return NotFound();
            }

            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            return Ok(await _repository.UpdateBasket(basket)); 
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);   
            return Ok();
        }

    }
}
