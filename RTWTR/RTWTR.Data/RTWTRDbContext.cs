using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RTWTR.Data.Models;

namespace RTWTR.Data
{
    public class RTWTRDbContext : IdentityDbContext<User>
    {
        public RTWTRDbContext(DbContextOptions<RTWTRDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Tweet> Tweets { get; set; }

        public DbSet<TwitterUserTweet> TwitterUserTweets { get; set; }

        public DbSet<Collection> Collections { get; set; }

        public DbSet<CollectionTweet> CollectionTweets { get; set; }

        public DbSet<UserTwitterUser> UserTwitterUsers { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<TwitterUserTweet>()
                .HasKey(x => new { x.TwitterUserId, x.TweetId});

            builder.Entity<TwitterUserTweet>()
                .HasOne(x => x.TwitterUser)
                .WithMany(x => x.TwitterUserTweets)
                .HasForeignKey(x => x.TwitterUserId);

            builder.Entity<TwitterUserTweet>()
                .HasOne(x => x.Tweet)
                .WithMany(x => x.TwitterUserTweets)
                .HasForeignKey(x => x.TweetId);

            builder.Entity<CollectionTweet>()
                .HasKey(x => new {x.TweetId, x.CollectionId});

            builder.Entity<CollectionTweet>()
                .HasOne(x => x.Tweet)
                .WithMany(x => x.CollectionTweets)
                .HasForeignKey(x => x.TweetId);

            builder.Entity<CollectionTweet>()
                .HasOne(x => x.Collection)
                .WithMany(x => x.CollectionTweets)
                .HasForeignKey(x => x.CollectionId);

            builder.Entity<UserTwitterUser>()
                .HasKey(x => new { x.TwitterUserId, x.UserId });

            builder.Entity<UserTwitterUser>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTwitterUsers)
                .HasForeignKey(x => x.UserId);

            builder.Entity<UserTwitterUser>()
                .HasOne(x => x.TwitterUser)
                .WithMany(x => x.UserTwitterUsers)
                .HasForeignKey(x => x.TwitterUserId);

            builder.Entity<UserTweets>()
                .HasKey(x => new { x.UserId, x.TweetId });

            builder.Entity<UserTweets>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTweets)
                .HasForeignKey(x => x.UserId);

            builder.Entity<UserTweets>()
                .HasOne(x => x.Tweet)
                .WithMany(x => x.UserTweets)
                .HasForeignKey(x => x.TweetId);
        }
    }
}
