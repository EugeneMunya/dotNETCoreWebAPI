using AutoMapper;
using dotNETCoreWebAPI.Models;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Dtos.Weapon;

namespace dotNETCoreWebAPI
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
        }
    }
}