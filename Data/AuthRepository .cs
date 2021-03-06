using System.Security.Claims;
using System.Collections.Generic;
using System;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Models;
using dotNETCoreWebAPI.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
namespace dotNETCoreWebAPI.Data
{
    public class AuthRepository : IAuthRepository
    {

        private readonly DataContext _context;
        private readonly IConfiguration _configuration;

        public AuthRepository(DataContext context, IConfiguration configuration)
        {
            _context=context;
            _configuration= configuration;
        } 

        public void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] hashSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                hashSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }

        }

        public bool VerifyPassword(string password, byte[]passwordHash, byte[]hashSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512(hashSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                 {
                     if(computedHash[i]!=passwordHash[i])
                     {
                         return false;
                     }

                 }
                 return true;
            }
        }
        public async Task<ServiceResponse<string>> Login(string username, string password)
        {
            ServiceResponse<string> response = new ServiceResponse<string>();
            User user = await _context.Users.FirstOrDefaultAsync(usr => usr.UserName.ToLower().Equals(username.ToLower()));
            if(user == null)
            {
                response.Success=false;
                response.Message="User not found";
            }
            else if(!VerifyPassword(password, user.PasswordHash, user.HasSalt))
            {
                response.Success=false;
                response.Message="Wrong password";
            }
            else
            {
                response.Success= true;
                response.Data= CreateToken(user);
            }

            return response;
        }

        public async Task<ServiceResponse<int>> Register(User user, string password)
        {
            ServiceResponse<int> serviceResponse = new ServiceResponse<int>();
            if(await UserExists(user.UserName))
            {
                serviceResponse.Success=false;
                serviceResponse.Message="User already exist";
                return serviceResponse;
            }
            GeneratePasswordHash(password, out byte[] passwordHash, out byte[] hashSalt);
            user.PasswordHash = passwordHash;
            user.HasSalt = hashSalt;
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();

            ServiceResponse<int> response = new ServiceResponse<int>();
            response.Data = user.Id;
            return response;
        }

        public async Task<bool> UserExists(string username)
        {
            if(await _context.Users.AnyAsync(user => user.UserName.ToLower()==username.ToLower()))
            {
                return true;
            }
            return false;
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>{
                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Role, user.Role),
            };

            SymmetricSecurityKey key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));
            SigningCredentials creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor{
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = creds
            };
            JwtSecurityTokenHandler securityHandler = new JwtSecurityTokenHandler();
            SecurityToken token = securityHandler.CreateToken(tokenDescriptor);

            return securityHandler.WriteToken(token);
        }
    }
}