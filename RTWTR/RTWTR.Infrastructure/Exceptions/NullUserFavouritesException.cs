using System;

namespace RTWTR.Infrastructure.Exceptions
{
    public class NullUserFavouritesException : ArgumentNullException
    {
        public NullUserFavouritesException()
        {
        }

        public NullUserFavouritesException(string message) 
            : base(message)
        {
        }
    }
}
