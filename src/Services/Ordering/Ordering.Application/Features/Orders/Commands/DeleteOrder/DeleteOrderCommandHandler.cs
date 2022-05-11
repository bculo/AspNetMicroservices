using MediatR;
using Microsoft.Extensions.Logging;
using Ordering.Application.Contracts.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Ordering.Application.Features.Orders.Commands.DeleteOrder
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _repository;
        private readonly ILogger<DeleteOrderCommandHandler> _logger;

        public DeleteOrderCommandHandler(IOrderRepository repo,
            ILogger<DeleteOrderCommandHandler> logger)
        {
            _repository = repo;
            _logger = logger;
        }

        public async Task<Unit> Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var order = await _repository.GetByIdAsync(request.Id);

            if(order is null)
            {
                throw new Exceptions.NotFoundException(order.Id);
            }

            await _repository.DeleteAsync(order);

            _logger.LogInformation("Order with ID {0} deleted.", order.Id);

            return Unit.Value;
        }
    }
}
