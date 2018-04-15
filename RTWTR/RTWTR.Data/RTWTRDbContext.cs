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

        // This WAS intended
        public new DbSet<User> Users { get; set; }

        public DbSet<Tweet> Tweets { get; set; }

        public DbSet<UserTweet> UserTweets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserTweet>()
                .HasKey(x => new { x.UserId, x.TweetId});

            builder.Entity<UserTweet>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTweets)
                .HasForeignKey(x => x.UserId);

            builder.Entity<UserTweet>()
                .HasOne(x => x.Tweet)
                .WithMany(x => x.UserTweets)
                .HasForeignKey(x => x.TweetId);
        }
    }
}
