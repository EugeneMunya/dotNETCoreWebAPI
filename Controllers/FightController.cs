using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Fight;
using dotNETCoreWebAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace dotNETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightController:ControllerBase
    {
        private readonly IFightService _fightService;

        public FightController(IFightService fightService)
        {
            _fightService=fightService;
        }


        [HttpPost("Weapon")]
        public async Task<IActionResult> WeaponAttack(WeaponAttackDto request)
        {
            return Ok(await _fightService.WeaponAttack(request));
        }
        [HttpPost("Skill")]
        public async Task<IActionResult> SkillAttack(SkillAttackDto request)
        {
            return Ok(await _fightService.SkillAttack(request));
        }
    }
}