using System.Collections.Generic;
using dotNETCoreWebAPI.Models;


namespace dotNETCoreWebAPI.Services
{
    public interface ICharacterService
    {
         List<Character> GetAllCharacters();
         Character GetCharacterById(int id);
         List<Character>AddNewCharacter(Character newChar);
    }
}