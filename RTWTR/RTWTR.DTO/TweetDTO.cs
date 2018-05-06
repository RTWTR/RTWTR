using Newtonsoft.Json;

namespace RTWTR.DTO
{
    public class TweetDto 
    {
        public string Id { get; set; }

        [JsonProperty("id")]
        public string TwitterId { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("user")]
        public TwitterUserDto TwitterUser { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}
