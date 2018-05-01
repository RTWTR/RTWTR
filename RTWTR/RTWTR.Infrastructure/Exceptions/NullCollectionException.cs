using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class NullCollectionException : ArgumentNullException
    {
        public NullCollectionException()
        {
        }

        public NullCollectionException(string message) 
            : base(message)
        {
        }
    }
}
