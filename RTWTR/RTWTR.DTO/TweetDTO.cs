using Newtonsoft.Json;

namespace RTWTR.DTO
{
    public class TweetDto 
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("text")]
        public string Text { get; set; }

        [JsonProperty("user")]
        public TwitterUserDto User { get; set; }

        [JsonProperty("created_at")]
        public string CreatedAt { get; set; }
    }
}
