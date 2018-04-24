using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class NullTwitterUserException : ArgumentNullException
    {
        public NullTwitterUserException()
        {
        }

        public NullTwitterUserException(string message) 
            : base(message)
        {
        }
    }
}
