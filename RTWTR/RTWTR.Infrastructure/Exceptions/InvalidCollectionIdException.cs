using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class InvalidCollectionIdException : ArgumentNullException
    {
        public InvalidCollectionIdException()
        {
        }

        public InvalidCollectionIdException(string message) 
            : base(message)
        {
        }
    }
}
