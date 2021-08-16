using System;

namespace Application.DataAccess.Exceptions
{
    public class NotFoundUserException: Exception
    {
        public NotFoundUserException(string message) : base(message)
        {
        }
    }
}