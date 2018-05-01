using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class NullUserException : ArgumentNullException
    {
        public NullUserException()
        {
        }

        public NullUserException(string message)
            : base(message)
        {
        }
    }
}
