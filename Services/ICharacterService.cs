using System.Collections.Generic;
using dotNETCoreWebAPI.Models;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;


namespace dotNETCoreWebAPI.Services
{
    public interface ICharacterService
    {
      Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters();
      Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id);
      Task<ServiceResponse<List<GetCharacterDto>>>AddNewCharacter(AddCharacterDto newChar);
      Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter);
      Task<ServiceResponse<List<GetCharacterDto>>> DeleteCharacter(int id);
    }
}