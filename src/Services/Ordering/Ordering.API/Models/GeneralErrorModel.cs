using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ordering.API.Models
{
    public class GeneralErrorModel
    {
        public string Title { get; set; }
        public string UserMessage { get; set; }
        public string DeveloperMessage { get; set; }
        public string ErrorUniqueIdentifier { get; set; }
    }
}
