using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Dtos.Weapon;

namespace dotNETCoreWebAPI.Services
{
    public interface IWeaponService
    {
             Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon);

    }
}