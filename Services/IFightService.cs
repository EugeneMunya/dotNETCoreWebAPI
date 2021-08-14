using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Fight;

namespace dotNETCoreWebAPI.Services
{
    public interface IFightService
    {
         Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request);
         Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request);
    }
}