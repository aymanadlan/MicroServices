

using AutoMapper;
using CommandsService.Dtos;
using CommandsService.Models;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CommandsService.Profiles
 {
    public class CommandsProfile : Profile
    {
        public CommandsProfile()
        {
            //source => Target
            CreateMap<Platform,PlatformReadDto>();
            CreateMap<CommandCreateDto,Command>();
            CreateMap<Command,CommandReadDto>();
        }
    }
 }