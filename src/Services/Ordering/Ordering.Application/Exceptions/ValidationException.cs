using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Application.Exceptions
{
    public class ValidationException : OrderingAppBaseException
    {
        public IDictionary<string, string[]> Errors { get; }

        public ValidationException() : base("One or more validation failures have occured.")
        {
            Errors = new Dictionary<string, string[]>();
        }

        public ValidationException(IEnumerable<ValidationFailure> failures) : this()
        {
            Errors = failures
                .GroupBy(e => e.PropertyName)
                .ToDictionary(x => x.Key, y => y.Select(i => i.ErrorMessage).ToArray());
        }


    }
}
