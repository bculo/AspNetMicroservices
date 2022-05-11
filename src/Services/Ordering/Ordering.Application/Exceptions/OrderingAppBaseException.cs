using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class OrderingAppBaseException : Exception
    {
        private string _userMessage = null;

        public string UserMessage
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_userMessage))
                {
                    return Message;
                }

                return _userMessage;
            }
            set
            {
                _userMessage = value;
            }
        }

        public OrderingAppBaseException()
        {
        }

        public OrderingAppBaseException(string message) : base(message)
        {
        }

        public OrderingAppBaseException(string message, string userMessage) : base(message)
        {
            _userMessage = userMessage;
        }
    }
}
