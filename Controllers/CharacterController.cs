using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using dotNETCoreWebAPI.Models;
using System.Linq;
using dotNETCoreWebAPI.Services;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace dotNETCoreWebAPI.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class CharacterController: ControllerBase
    {
        private readonly ICharacterService _characterService;
        public CharacterController(ICharacterService characterService)
        {
            _characterService = characterService;
        }

        [HttpGet("GetAll")]
        public async  Task<IActionResult> Get()
    {
        int userId = int.Parse(User.Claims.FirstOrDefault(clm => clm.Type == ClaimTypes.NameIdentifier).Value);
        return Ok( await _characterService.GetAllCharacters(userId));

    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetSingle( int id)
    {
        return Ok(await _characterService.GetCharacterById(id));
    }
    [HttpPost]
    public async Task<IActionResult> AddCharacter(AddCharacterDto newCharacter)
    {
        return Ok(await _characterService.AddNewCharacter(newCharacter));
        
    }

    [HttpPut]
    public async Task<IActionResult> UpdateCharacter(UpdateCharacterDto updatedCharacter)
    {
        ServiceResponse<GetCharacterDto> response = await _characterService.UpdateCharacter(updatedCharacter);
        if(response.Data == null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCharacter(int id)
    {
        ServiceResponse<List<GetCharacterDto>> response = await _characterService.DeleteCharacter(id);

        if(response.Data == null)
        {
            return NotFound(response);
        }
        return Ok(response);
    }
    }
    
}