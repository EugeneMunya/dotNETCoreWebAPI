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
    }
    
}