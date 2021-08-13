using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Weapon;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNETCoreWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeaponController:ControllerBase
    {
        private readonly IWeaponService _weapon;

        public WeaponController(IWeaponService weapon)
        {
            _weapon = weapon;
        }

        [HttpPost]
        public async Task<IActionResult> AddNewWeapon(AddWeaponDto weaponRequest)
        {
            ServiceResponse<GetCharacterDto> response = await _weapon.AddWeapon(weaponRequest);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        
    }
}