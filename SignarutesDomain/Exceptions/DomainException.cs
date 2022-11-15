using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Signarutes.Domain.Contracts.Exceptions
{
    /** acts as a base class for domain exceptions. Is used to determine a set of known domain exceptions */
    public abstract class DomainException : Exception
    {
        protected DomainException() { }

        protected DomainException(string message) : base(message) { }

        protected DomainException(string message, Exception innerException) : base(message, innerException) { }
    }
}
