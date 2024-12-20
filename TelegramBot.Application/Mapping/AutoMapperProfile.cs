using AutoMapper;
using TelegramBot.Application.DTO;
using TelegramBot.Application.Entities;

namespace TelegramBot.Application.Mapping;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<UserDTO, User>();
    }
}