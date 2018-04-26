using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class InvalidTweetIdException : ArgumentException
    {
        public InvalidTweetIdException()
        {
        }

        public InvalidTweetIdException(string message) 
            : base(message)
        {
        }
    }
}
