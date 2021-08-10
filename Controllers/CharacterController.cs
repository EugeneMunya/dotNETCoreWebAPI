using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using dotNETCoreWebAPI.Models;
using System.Linq;
using dotNETCoreWebAPI.Services;



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
        public IActionResult Get()
    {
        return Ok(_characterService.GetAllCharacters());

    }

    [HttpGet("{id}")]

    public IActionResult GetSingle( int id)
    {
        return Ok(_characterService.GetCharacterById(id));
    }
    [HttpPost]
    public IActionResult AddCharacter(Character newCharacter)
    {
        return Ok(_characterService.AddNewCharacter(newCharacter));
        
    }
    }
    
}