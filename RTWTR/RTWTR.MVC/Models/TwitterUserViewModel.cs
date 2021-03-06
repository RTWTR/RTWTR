﻿namespace RTWTR.MVC.Models
{
    public class TwitterUserViewModel
    {
        public string Id { get; set; }

        public string TwitterId { get; set; }

        public string Name { get; set; }

        public string ScreenName { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

        public bool IsFavourite { get; set; }
    }
}
