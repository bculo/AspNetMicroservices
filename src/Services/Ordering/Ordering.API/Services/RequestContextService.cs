using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Services
{
    public class RequestContextService : IRequestContextService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly ILogger<RequestContextService> _logger;

        public string UniqueRequestId { get; private set; }

        private string _clientIpAddress = null;
        public string ClientIpAddress
        {
            get
            {
                if (_clientIpAddress == null)
                {
                    _clientIpAddress = GetClientIpAddress();
                }

                return _clientIpAddress;
            }
        }

        public RequestContextService(IHttpContextAccessor accessor, ILogger<RequestContextService> logger)
        {
            _httpContextAccessor = accessor;
            _logger = logger;

            UniqueRequestId = System.Diagnostics.Activity.Current.Id;
        }

        private string GetClientIpAddress()
        {
            if (_httpContextAccessor?.HttpContext?.Request == null)
            {
                _logger.LogTrace("HttpContext Reuqest instance is NULL, IP unvailable");
                return null;
            }

            var result = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress.ToString();

            _logger.LogTrace("Client IP address found {0}", result);

            return result;
        }
    }
}
