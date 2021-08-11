using AutoMapper;
using dotNETCoreWebAPI.Models;
using dotNETCoreWebAPI.Dtos.Character;

namespace dotNETCoreWebAPI
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
        }
    }
}