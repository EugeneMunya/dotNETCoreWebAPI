using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Dtos.CharacterSkill;
using dotNETCoreWebAPI.Services;
using dotNETCoreWebAPI.Services.CharacterSkillService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace dotNETCoreWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterSkillController:ControllerBase
    {
        private readonly ICharacterSkillService _characterSkillService;
        public CharacterSkillController(ICharacterSkillService characterSkillService)
        {
            _characterSkillService=characterSkillService;
        }

        public async Task<IActionResult> AddNewCharacterSkill(AddCharacterSkillDto newCharacterSkill)
        {
         ServiceResponse<GetCharacterDto> response = await _characterSkillService.AddCharacterSkill(newCharacterSkill);
         if(!response.Success)
         {
             return BadRequest(response);
         }  
         return Ok(response) ;
        }
        
    }
}