using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping;
using StackExchange.Redis;

namespace RTWTR.DTO
{
    public class CollectionDTO : IMapFrom<Collection>
    {
        public string Name { get; set; }

        public ICollection<TweetDto> CollectionTweets { get; set; }

    }
}
