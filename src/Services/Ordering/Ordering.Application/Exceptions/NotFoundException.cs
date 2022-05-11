using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class NotFoundException : OrderingAppBaseException
    {
        public NotFoundException() : base("Item not found!")
        {
        }

        public NotFoundException(int itemId) : base($"Item with identifier {itemId} not found!")
        {
        }

        public NotFoundException(string message) : base(message, message)
        {
        }
    }
}
