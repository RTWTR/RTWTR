using RTWTR.Data.Access.Contracts;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.Infrastructure.Mapping.Provider;
using RTWTR.Service.Data.Contracts;
using System.Linq;

namespace RTWTR.Service.Data
{
    public class TweetService : ITweetService
    {
        private readonly ISaver saver;
        private readonly IMappingProvider mapper;
        private readonly IRepository<Tweet> tweets;


        public TweetService(ISaver saver, IMappingProvider mapper, IRepository<Tweet> tweets)
        {
            this.saver = saver;
            this.mapper = mapper;
            this.tweets = tweets;
        }

        public TweetDto GetTweetById (string tweetId)
        {
            if (string.IsNullOrEmpty(tweetId))
            {
                return null;
            }

            var tweet = tweets.All.Where(x => x.Id == tweetId);

            return mapper.MapTo<TweetDto>(tweet);

        }

        public int AddTweet(Tweet tweetToSave)
        {
            if (tweetToSave == null)
            {
                return -1;
            }

            this.tweets.Add(tweetToSave);

            return this.saver.SaveChanges();
        }

    }
}
