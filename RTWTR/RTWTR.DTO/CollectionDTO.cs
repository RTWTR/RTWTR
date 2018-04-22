using System.Collections.Generic;

namespace RTWTR.DTO
{
    public class CollectionDTO
    {
        public string Name { get; set; }

        public ICollection<TweetDto> CollectionTweets { get; set; }
    }
}
