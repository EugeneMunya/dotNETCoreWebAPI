using Microsoft.AspNetCore.Mvc;
using dotNETCoreWebAPI.Data;
using dotNETCoreWebAPI.Dtos.User;
using System.Threading.Tasks;
using dotNETCoreWebAPI.Services;
using dotNETCoreWebAPI.Models;


namespace dotNETCoreWebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IAuthRepository _authRepo;

        public AuthController(IAuthRepository authRepo)
        {
            _authRepo=authRepo;
        }
        
        [HttpPost("register")]
        public async Task<IActionResult> UserRegister(UserRegisterDto request)
        {
         ServiceResponse<int> response = await _authRepo.Register(
             new User{UserName=request.UserName},
             request.Password
         ) ;
         if(!response.Success)
         {
             return BadRequest(response);
         }
         return Ok(response);
        }

        [HttpPost("Login")]
        public async Task<IActionResult> userLogin(UserLoginDto request)
        {
            ServiceResponse<string> response = await _authRepo.Login(request.UserName,request.Password);
            if(!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
        
    }
}