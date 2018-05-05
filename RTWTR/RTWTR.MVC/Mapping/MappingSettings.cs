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
            this.CreateMap<TwitterUserDto, TwitterUserViewModel>(MemberList.Source);
            this.CreateMap<UserDTO, User>(MemberList.Source);
            this.CreateMap<TwitterUserDto, TwitterUser>(MemberList.Source);
        }
    }
}
