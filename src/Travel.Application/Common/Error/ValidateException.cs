using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Travel.Application.Common.Error
{
    public class ValidateException:Exception
    {

        public IDictionary<string, string[]> Failures { get; set; }
        public ValidateException()
            : base("One or more validation failure have occured.")
        {
            Failures = new Dictionary<string, string[]>();
        }
        public ValidateException(IList<ValidationFailure> failures)
            : this()
        {
            var failureGroups = failures
                .GroupBy(e => e.PropertyName, e => e.ErrorMessage);

            foreach (var failureGroup in failureGroups)
            {
                var propertyName = failureGroup.Key;
                var propertyFailures = failureGroup.ToArray();

                Failures.Add(propertyName, propertyFailures);
            }
        }
    }
}
