using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping;

namespace RTWTR.DTO
{
    public class TweetDto : IMapFrom<Tweet>
    {
        public string Text { get; set; }

        public TwitterUser User { get; set; }

        public string CreatedAt { get; set; }

    }
}
