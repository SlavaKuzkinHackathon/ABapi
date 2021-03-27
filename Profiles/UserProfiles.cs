using AutoMapper;
using ABapi.Dtos;
using ABapi.Models;

namespace ABapi.Profiles {
    public class UsersProfile : Profile {
        public UsersProfile () {
            //Source -> Target
            CreateMap<User, UserReadDto> ();
            CreateMap<UserCreateDto, User> ();
            CreateMap<UserUpdateDto, User> ();
            CreateMap<User, UserUpdateDto> ();
        }

    }

}