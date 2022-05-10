using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Infrastructure;
using Ordering.Application.Contracts.Persistence;
using Ordering.Application.Models;
using Ordering.Domain.Entitites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.CheckoutOrder
{
    public class CheckoutOrderCommandHandler : IRequestHandler<CheckoutOrderCommand, int>
    {
        private readonly IMapper _mapper;
        private readonly IOrderRepository _repo;
        private readonly IEmailService _emailService;
        private readonly ILogger<CheckoutOrderCommandHandler> _logger;

        public CheckoutOrderCommandHandler(IMapper mapper, 
            IOrderRepository repo, 
            IEmailService emailService,
            ILogger<CheckoutOrderCommandHandler> logger)
        {
            _mapper = mapper;
            _repo = repo;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<int> Handle(CheckoutOrderCommand request, CancellationToken cancellationToken)
        {
            var orderEntity = _mapper.Map<Order>(request);

            var newOrder = await _repo.AddAsync(orderEntity);

            _logger.LogInformation("order {0} is successfully created.", newOrder.Id);

            await SendEmail(newOrder);

            return newOrder.Id;
        }

        private async Task SendEmail(Order newOrder)
        {
            var email = new Email
            {
                To = "hardcorenoob2@gmail.com",
                Subject = "New order",
                Body = "Order was created"
            };

            await _emailService.SendEmail(email);
        }
    }
}
