using System.Collections.Generic;
using System.Linq;
using dotNETCoreWebAPI.Models;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using AutoMapper;

namespace dotNETCoreWebAPI.Services
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;

        public CharacterService(IMapper mapper)
        {
          _mapper=mapper;  
        }

         private static List<Character> characters = new List<Character>() {
            new Character(),
            new Character{Name="Sam", Id=1}
        };
        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newChar)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = _mapper.Map<Character>(newChar);
                newCharacter.Id = characters.Max(c => c.Id) + 1;
             characters.Add(newCharacter);
             serviceResponse.Data= characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
        return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            serviceResponse.Data=characters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character character = characters.FirstOrDefault(c=>c.Id==id);
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(characters.FirstOrDefault(character=>character.Id==id));
            return serviceResponse;
        }
    }
}