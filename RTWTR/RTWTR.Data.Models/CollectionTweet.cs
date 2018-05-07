using System;
using RTWTR.Data.Models.Contracts;

namespace RTWTR.Data.Models
{
    public class CollectionTweet : IDeletable, IAuditable
    {
        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }

        public string CollectionId { get; set; }

        public Collection Collection { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public DateTime? CreatedOn { get; set; }

        public DateTime? UpdatedOn { get; set; }
    }
}
