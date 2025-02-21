using AutoMapper;
using UserApplication.Dtos;
using UserApplication.Entities.TestDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserApplication.Helpers
{
    public class UsersMappingProfile : Profile
    {
        public UsersMappingProfile()
        {
            CreateMap<Users, UserDto.User>();
            CreateMap<UserDto.User, Users >();
        }
    }
}
