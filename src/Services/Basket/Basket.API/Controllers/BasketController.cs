using AutoMapper;
using Basket.API.Entities;
using Basket.API.Repositories;
using Basket.API.Services;
using EventBus.Messages.Events;
using MassTransit;
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
        private readonly IDiscountService _service;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publish;
        public BasketController(IBasketRepository repository, IDiscountService service, IMapper mapper, IPublishEndpoint publish)
        {
            _repository = repository;
            _service = service;
            _mapper = mapper;
            _publish = publish;
        }

        [HttpGet("{username}")]
        public async Task<IActionResult> GetBasket(string username)
        {
            var basket = await _repository.GetBasket(username);

            if(basket is null)
            {
                return Ok(new ShoppingCart(username));
            }

            return Ok(basket);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket([FromBody] ShoppingCart basket)
        {
            foreach(var item in basket.Items)
            {
                var coupon = await _service.GetDiscount(item.ProductName);
                item.Price -= coupon.Amount;
            }

            return Ok(await _repository.UpdateBasket(basket)); 
        }

        [HttpDelete("{userName}")]
        public async Task<IActionResult> DeleteBasket(string username)
        {
            await _repository.DeleteBasket(username);   
            return Ok();
        }

        [HttpPost("[action]", Name = "Checkout")]
        public async Task<IActionResult> Checkout([FromBody] BasketCheckout basketCheckout)
        {
            var basket = await _repository.GetBasket(basketCheckout.UserName);
            if(basket is null)
            {
                return BadRequest();
            }

            var eventMessage = _mapper.Map<BasketCheckoutEvent>(basketCheckout);
            eventMessage.TotalPrice = basket.TotalPrice;
            await _publish.Publish(eventMessage);

            await _repository.DeleteBasket(basket.UserName);

            return Accepted();
        }

    }
}
