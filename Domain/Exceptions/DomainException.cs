using System;

namespace Domain.Exceptions
{
    /// <summary>
    /// Represents an error that occurs due to a violation of a domain business rule.
    /// </summary>
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}