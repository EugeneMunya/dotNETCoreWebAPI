using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using dotNETCoreWebAPI.Data;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Dtos.CharacterSkill;
using dotNETCoreWebAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace dotNETCoreWebAPI.Services.CharacterSkillService
{
    public class CharacterSkillService : ICharacterSkillService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;

        public CharacterSkillService(DataContext context,IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _context = context;
        }
        public async Task<ServiceResponse<GetCharacterDto>> AddCharacterSkill(AddCharacterSkillDto characterSkill)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();

            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == characterSkill.CharacterId && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(character == null)
                {
                    response.Success=false;
                    response.Message="Character not found";
                    return response;
                }
                Skill skill = await _context.Skills.FirstOrDefaultAsync(s => s.Id == characterSkill.SkillId);
                if(skill==null)
                {
                    response.Success=false;
                    response.Message="Skill not found";
                    return response;
                }

                CharacterSkill newCharacterSkill = new CharacterSkill{
                    Character = character,
                    Skill = skill

                };
                await _context.CharacterSkills.AddAsync(newCharacterSkill);
                await _context.SaveChangesAsync();
                response.Data = _mapper.Map<GetCharacterDto>(character);
            }
            catch (System.Exception ex)
            {
                 response.Success=false;
                 response.Message=ex.Message;
            }

            return response;

            
        }
    }
}