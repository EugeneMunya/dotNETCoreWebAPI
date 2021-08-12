using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using dotNETCoreWebAPI.Models;
using System.Linq;
using dotNETCoreWebAPI.Services;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;



namespace dotNETCoreWebAPI.Controllers
{

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
        return Ok( await _characterService.GetAllCharacters());

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