using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Services
{
    public interface IRequestContextService
    {
        string UniqueRequestId { get; }
        string ClientIpAddress { get; }
    }
}
