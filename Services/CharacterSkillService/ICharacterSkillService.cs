using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Dtos.CharacterSkill;

namespace dotNETCoreWebAPI.Services.CharacterSkillService
{
    public interface ICharacterSkillService
    {
         public Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto characterSkill);
    }
}