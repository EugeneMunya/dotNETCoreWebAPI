using System.Collections.Generic;
using System.Linq;
using dotNETCoreWebAPI.Models;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using AutoMapper;
using dotNETCoreWebAPI.Data;
using Microsoft.EntityFrameworkCore;




namespace dotNETCoreWebAPI.Services
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context)
        {
          _mapper=mapper;
          _context= context;  
        }

         private static List<Character> characters = new List<Character>() {
            new Character(),
            new Character{Name="Sam", Id=1}
        };
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newChar)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = _mapper.Map<Character>(newChar);
             await _context.characters.AddAsync(newCharacter);
             await _context.SaveChangesAsync();
             serviceResponse.Data= await _context.characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> Dbcharacters = await _context.characters.ToListAsync();
            serviceResponse.Data=Dbcharacters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character DbCharacter =  await _context.characters.FirstOrDefaultAsync(c => c.Id==id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(DbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
         try
         {
            Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            character.Name = updatedCharacter.Name;
            character.Class = updatedCharacter.Class;
            character.Defence = updatedCharacter.Defense;
            character.HitPoint = updatedCharacter.HitPoints;
            character.Intelligency = updatedCharacter.Intelligence;
            character.Strength = updatedCharacter.Strength;
            _context.characters.Update(character);
            await _context.SaveChangesAsync();
            serviceResponse.Data=_mapper.Map<GetCharacterDto>(character);
         }
         catch (System.Exception ex)
         {
             serviceResponse.Success= false;

             serviceResponse.Message= ex.Message;
              
         }
           
            return serviceResponse;
            

        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
           try
           {
            Character character = await _context.characters.FirstAsync(c => c.Id == id);
             _context.characters.Remove(character);
             await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
               
           }
           catch (System.Exception ex)
           {
                serviceResponse.Success= false;
                serviceResponse.Message = ex.Message;
           }
            

            return serviceResponse;

        }
    }
}