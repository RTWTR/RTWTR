using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RTWTR.Data.Models;

namespace RTWTR.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        // hiding WAS intended
        public new DbSet<User> Users { get; set; }

        public DbSet<Tweet> Tweets { get; set; }

        public DbSet<TwitterUser> TwitterUsers { get; set; }

        public DbSet<UserTweet> UserTweets { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Add composite key
            builder.Entity<UserTweet>()
                .HasKey(x => new { x.UserId, x.TweetId });

            builder.Entity<UserTweet>()
                .HasOne(x => x.User)
                .WithMany(x => x.UserTweets)
                .HasForeignKey(x => x.UserId);

            builder.Entity<UserTweet>()
                .HasOne(x => x.Tweet)
                .WithMany(x => x.UserTweets)
                .HasForeignKey(x => x.TweetId);

            base.OnModelCreating(builder);
        }
    }
}
