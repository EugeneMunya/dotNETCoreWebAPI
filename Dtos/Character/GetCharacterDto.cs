using System.Collections.Generic;
using dotNETCoreWebAPI.Dtos.Skill;
using dotNETCoreWebAPI.Dtos.Weapon;
using dotNETCoreWebAPI.Models;

namespace dotNETCoreWebAPI.Dtos.Character
{
    public class GetCharacterDto
    {
         public int Id { get; set; }
        public string Name { get; set; } = "Frodo";
        public int HitPoints { get; set; } = 100;
        public int Strength { get; set; } = 10;
        public int Defense { get; set; } = 10;
        public int Intelligence { get; set; } = 10;
        public RpgClass Class { get; set; } = RpgClass.Knight;
        public GetWeaponDto Weapon { get; set; }
        public List<GetSkillDto> Skills { get; set; }
    }
}
