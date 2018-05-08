using Newtonsoft.Json;

namespace RTWTR.DTO
{
    public class TwitterUserDto
    {
        [JsonIgnore]
        public string Id { get; set; }

        [JsonProperty("id_str")]
        public string TwitterId { get; set; }

        [JsonProperty("screen_name")]
        public string ScreenName { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("profile_image_url")]
        public string ProfileImageUrl { get; set; }
    }
}
