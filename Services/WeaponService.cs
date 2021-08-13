using System.Threading.Tasks;
using dotNETCoreWebAPI.Dtos.Character;
using dotNETCoreWebAPI.Dtos.Weapon;
using dotNETCoreWebAPI.Data;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using dotNETCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace dotNETCoreWebAPI.Services
{
    public class WeaponService : IWeaponService
    {

        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IMapper _mapper;
        public WeaponService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _context=context;
            _httpContextAccessor= httpContextAccessor;
            _mapper= mapper;
        }

        public async Task<ServiceResponse<GetCharacterDto>> AddWeapon(AddWeaponDto newWeapon)
        {
            ServiceResponse<GetCharacterDto> response = new ServiceResponse<GetCharacterDto>();
            try
            {
                Character character = await _context.characters.FirstOrDefaultAsync(c => c.Id == newWeapon.CharacterId && c.User.Id == int.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
                if(character == null)
                {
                    response.Success=false;
                    response.Message="Character not found";
                    return response;
                }
                else
                {
                    Weapon weapon = new Weapon
                    {
                        Name = newWeapon.Name,
                        Damage = newWeapon.Damage,
                        character = character
                    };
                    await _context.Weapons.AddAsync(weapon);
                    await _context.SaveChangesAsync();
                    response.Data = _mapper.Map<GetCharacterDto>(character);

                }
            }
            catch (System.Exception ex)
            {
                 response.Success=false;
                 response.Message= ex.Message;
            }

            return response;
        }
    }
}