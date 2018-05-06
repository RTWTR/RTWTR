using System;
using AutoMapper;
using RTWTR.Data.Models;
using RTWTR.DTO;
using RTWTR.MVC.Models;

namespace RTWTR.MVC.Mapping
{
    public class MappingSettings : Profile
    {
        public MappingSettings()
        {
            // TODO: Maybe fill this?
            this.CreateMap<TwitterUserDto, TwitterUserViewModel>(MemberList.Source).ReverseMap();

            this.CreateMap<UserDTO, User>(MemberList.Source).ReverseMap();

            this.CreateMap<TwitterUserDto, TwitterUser>(MemberList.Source).ReverseMap();

            this.CreateMap<TweetDto, TweetViewModel>()
                .ForMember(
                    x => x.Id,
                    y => y.MapFrom(z => z.TwitterId))
                .ForMember(
                    x => x.TwitterUserId,
                    y => y.MapFrom(z => z.TwitterUser.TwitterId))
                .ForMember(
                    x => x.TwitterUserProfileImageUrl,
                    y => y.MapFrom(z => z.TwitterUser.ProfileImageUrl))
                .ForMember(
                    x => x.TwitterUserName,
                    y => y.MapFrom(z => z.TwitterUser.Name))
                .ForMember(
                    x => x.TwitterUserScreenName,
                    y => y.MapFrom(z => z.TwitterUser.ScreenName))
                .ReverseMap();

            this.CreateMap<TweetDto, Tweet>(MemberList.Destination)
                .ReverseMap();
        }
    }
}
