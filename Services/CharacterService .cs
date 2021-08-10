using System.Collections.Generic;
using System.Linq;
using dotNETCoreWebAPI.Models;

namespace dotNETCoreWebAPI.Services
{
    public class CharacterService : ICharacterService
    {

         private static List<Character> characters = new List<Character>() {
            new Character(),
            new Character{Name="Sam", Id=1}
        };
        public List<Character> AddNewCharacter(Character newChar)
        {
             characters.Add(newChar);
        return characters;
        }

        public List<Character> GetAllCharacters()
        {
            return characters;
        }

        public Character GetCharacterById(int id)
        {
            Character character = characters.FirstOrDefault(c=>c.Id==id);
            return character;
        }
    }
}