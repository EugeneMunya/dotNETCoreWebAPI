using System.Collections.Generic;
using System.Linq;
using dotNETCoreWebAPI.Models;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using AutoMapper;
using dotNETCoreWebAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace dotNETCoreWebAPI.Services
{
    public class CharacterService : ICharacterService
    {

        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContext;
        private readonly DataContext _context;

        public CharacterService(IMapper mapper, DataContext context, IHttpContextAccessor httpContextAccessor)
        {
          _mapper=mapper;
          _context= context;  
          _httpContext = httpContextAccessor;
        }

        private int GetUserId() => int.Parse(_httpContext.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));

        private string GetUserRole() => _httpContext.HttpContext.User.FindFirstValue(ClaimTypes.Role);

        public async Task<ServiceResponse<List<GetCharacterDto>>> AddNewCharacter(AddCharacterDto newChar)
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            Character newCharacter = _mapper.Map<Character>(newChar);
            newCharacter.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());
             await _context.characters.AddAsync(newCharacter);
             await _context.SaveChangesAsync();
             serviceResponse.Data= await _context.characters.Where(c=> c.User.Id==GetUserId()).Select(c=>_mapper.Map<GetCharacterDto>(c)).ToListAsync();
        return serviceResponse;
        }

        public async Task<ServiceResponse<List<GetCharacterDto>>> GetAllCharacters()
        {
            ServiceResponse<List<GetCharacterDto>> serviceResponse = new ServiceResponse<List<GetCharacterDto>>();
            List<Character> Dbcharacters = GetUserRole().Equals("Admin") ? 
             await _context.characters.ToListAsync() : await _context.characters.Where(c => c.User.Id == GetUserId()).ToListAsync();
            serviceResponse.Data=Dbcharacters.Select(c=>_mapper.Map<GetCharacterDto>(c)).ToList();
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> GetCharacterById(int id)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
            Character DbCharacter =  await _context.characters.FirstOrDefaultAsync(c => c.Id==id && c.User.Id == GetUserId());
            serviceResponse.Data = _mapper.Map<GetCharacterDto>(DbCharacter);
            return serviceResponse;
        }

        public async Task<ServiceResponse<GetCharacterDto>> UpdateCharacter(UpdateCharacterDto updatedCharacter)
        {
            ServiceResponse<GetCharacterDto> serviceResponse = new ServiceResponse<GetCharacterDto>();
         try
         {
            Character character = await _context.characters.Include(c => c.User).FirstOrDefaultAsync(c => c.Id == updatedCharacter.Id);
            if(character.User.Id == GetUserId())
            {
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
            else
            {
                serviceResponse.Success= false;
                serviceResponse.Message="Character not found";
            }
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
            Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == id &&  c.User.Id == GetUserId());
            if(character != null)
            {
            _context.characters.Remove(character);
             await _context.SaveChangesAsync();
            serviceResponse.Data = await _context.characters.Select(c => _mapper.Map<GetCharacterDto>(c)).ToListAsync();
            }
            else
            {
                serviceResponse.Success=false;
                serviceResponse.Message="Character not found";
            }
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