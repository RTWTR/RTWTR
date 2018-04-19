using RTWTR.Data.Models;
using RTWTR.Infrastructure.Mapping;

namespace RTWTR.DTO
{
    class TwitterUserDto : IMapFrom<TwitterUser>
    {
        public string ScreenName { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ProfileImageUrl { get; set; }

    }
}
