using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class InvalidTwitterUserIdException : ArgumentException
    {
        public InvalidTwitterUserIdException()
        {
        }

        public InvalidTwitterUserIdException(string message) 
            : base(message)
        {
        }
    }
}
