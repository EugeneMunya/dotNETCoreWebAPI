using System;
using System.Linq;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Data;
using dotNETCoreWebAPI.Dtos.Fight;
using dotNETCoreWebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace dotNETCoreWebAPI.Services
{
    public class FightService:IFightService
    {
        private readonly DataContext _context;
        public FightService(DataContext context)
        {
            _context=context;
        }

        public async Task<ServiceResponse<AttackResultDto>> SkillAttack(SkillAttackDto request)
        {
             ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto>();
           try
           {
               Character attacker = await _context.characters.Include(c => c.CharacterSkills).ThenInclude(cs => cs.Skill).FirstOrDefaultAsync(c => c.Id == request.AttackerId);
               Character opponent = await _context.characters.FirstOrDefaultAsync(c => c.Id == request.OpponentId);
               CharacterSkill characterSkill =
               attacker.CharacterSkills.FirstOrDefault(cs => cs.Skill.Id == request.SkillId);
               if (characterSkill == null)
               {
                  response.Success = false;
                  response.Message = $"{attacker.Name} doesn't know that skill.";
                  return response;
               }
               int damage = characterSkill.Skill.Damage + (new Random().Next(attacker.Intelligence));
               damage -= new Random().Next(opponent.Defense);
               if (damage > 0)
                    opponent.HitPoint -= (int)damage;
                if (opponent.HitPoint <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";
               
           }
           catch (System.Exception ex)
           {
                response.Success = false;
                response.Message = ex.Message;
           }
        }

        public async Task<ServiceResponse<AttackResultDto>> WeaponAttack(WeaponAttackDto request)
        {
            ServiceResponse<AttackResultDto> response = new ServiceResponse<AttackResultDto>();
            try
            {
                Character attacker = await _context.characters.Include(c => c.weapon).FirstOrDefaultAsync(c => c.Id == request.AttackerId);
                Character opponent = await _context.characters.FirstOrDefaultAsync(c => c.Id == request.OpponentId);
                int damage = attacker.weapon.Damage + (new Random().Next(attacker.Strength));
                damage -= new Random().Next(opponent.Defence);
                if (damage > 0)
                    opponent.HitPoint -= (int)damage;
                if (opponent.HitPoint <= 0)
                    response.Message = $"{opponent.Name} has been defeated!";
                
                _context.characters.Update(opponent);
                await _context.SaveChangesAsync();

                response.Data = new AttackResultDto
                {
                   Attacker = attacker.Name,
                   AttackerHP = attacker.HitPoint,
                   Opponent = opponent.Name,
                   OpponentHP = opponent.HitPoint,
                   Damage = damage
                };
                
            }
            catch (System.Exception ex)
            {
                 response.Success = false;
                 response.Message = ex.Message;
            }

            return response;
        }
    }
}