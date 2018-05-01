namespace RTWTR.Data.Models
{
    public class UserTweets
    {
        public string UserId { get; set; }

        public User User { get; set; }

        public string TweetId { get; set; }

        public Tweet Tweet { get; set; }
    }
}
