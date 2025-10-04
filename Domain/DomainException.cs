using System;

namespace PraticaPoo.Domain
{
    /// <summary>
    /// Represents errors that occur when a domain rule is violated. This exception should be
    /// thrown when an operation fails due to business logic restrictions rather than a
    /// programming error. For example, attempting to withdraw more stock than available.
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException() : base()
        {
        }

        public DomainException(string message) : base(message)
        {
        }

        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
