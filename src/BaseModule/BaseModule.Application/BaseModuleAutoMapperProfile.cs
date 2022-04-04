using AutoMapper;
using BaseModule.CommandHandlers;
using BaseModule.Contracts;
using CoreModule.Domain.Users;

namespace BaseModule
{
    public class BaseModuleAutoMapperProfile : Profile
    {
        public BaseModuleAutoMapperProfile()
        {
            CreateMap<CreateUserCommand, User>();
            CreateMap<User, CreateUserCommand>();

            CreateMap<UserDto, User>();
            CreateMap<User, UserDto>();
        }
    }
}
