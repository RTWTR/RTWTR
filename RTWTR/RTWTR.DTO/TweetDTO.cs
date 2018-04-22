﻿using RTWTR.Data.Models;

namespace RTWTR.DTO
{
    public class TweetDto 
    {
        public string Id { get; set; }
        
        public string Text { get; set; }

        public TwitterUserDto User { get; set; }

        public string CreatedAt { get; set; }
    }
}
