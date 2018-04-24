using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class InvalidUserIdException : ArgumentException
    {
        public InvalidUserIdException()
        {
        }

        public InvalidUserIdException(string message) 
            : base(message)
        {
        }
    }
}
