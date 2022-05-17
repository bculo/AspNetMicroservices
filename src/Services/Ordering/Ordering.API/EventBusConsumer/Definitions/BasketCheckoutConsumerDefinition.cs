using EventBus.Messages.Common;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.EventBusConsumer.Definitions
{
    public class BasketCheckoutConsumerDefinition : ConsumerDefinition<BasketCheckoutConsumer>
    {
        public BasketCheckoutConsumerDefinition()
        {
            EndpointName = EventBusConstants.BasketCheckoutQueue;
        }
    }
}
