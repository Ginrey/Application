using System;

namespace Application.DataAccess.Exceptions
{
    public class UserAlreadyRegisteredException: Exception
    {
        public UserAlreadyRegisteredException(string message) : base(message)
        {
        }
    }
}