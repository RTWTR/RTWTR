using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class NullTweetException : ArgumentNullException
    {
        public NullTweetException()
        {
        }

        public NullTweetException(string message) 
            : base(message)
        {
        }
    }
}
