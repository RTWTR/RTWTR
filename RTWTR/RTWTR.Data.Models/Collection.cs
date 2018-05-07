using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using RTWTR.Data.Models.Abstractions;

namespace RTWTR.Data.Models
{
    public class Collection : DataModel
    {
        public Collection()
        {
            this.CollectionTweets = new List<CollectionTweet>();
        }

        [Required]
        public string Name { get; set; }

        [Required]
        public string UserId { get; set; }

        public User User { get; set; }

        public ICollection<CollectionTweet> CollectionTweets { get; set; }
    }
}
