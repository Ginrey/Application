using System;

namespace Application.DataAccess.Exceptions
{
    public class IncorrectLoginException : Exception
    {
        public IncorrectLoginException(string message) : base(message)
        {
        }
    }
}