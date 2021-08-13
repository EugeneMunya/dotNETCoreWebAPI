using AutoMapper;
using dotNETCoreWebAPI.Models;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Dtos.Weapon;
using dotNETCoreWebAPI.Dtos.Skill;
using System.Linq;

namespace dotNETCoreWebAPI
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Character, GetCharacterDto>();
            CreateMap<AddCharacterDto, Character>();
            CreateMap<Weapon, GetWeaponDto>();
            CreateMap<Skill, GetSkillDto>();
            CreateMap<Character, GetCharacterDto>().ForMember(dto => dto.Skills, c => c.MapFrom(c => c.CharacterSkills.Select(cs => cs.Skill)));
        }
    }
}