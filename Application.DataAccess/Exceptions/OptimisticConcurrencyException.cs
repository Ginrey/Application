using System;

namespace Application.DataAccess.Exceptions
{
    public sealed class OptimisticConcurrencyException : Exception
    {
        public OptimisticConcurrencyException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}