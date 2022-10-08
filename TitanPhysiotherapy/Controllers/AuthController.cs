using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Services.UserService;
using TitanPhysiotherapy.Models.UserModels;

namespace TitanPhysiotherapy.Controllers
{
    [Route("api/Authentication")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authService;
        public AuthController(IAuthInterface authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<User>>> Register(UserRegisterDto request)
        {
            return Ok(await _authService.Register(request));
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login (UserDto request)
        {
            return Ok(await _authService.Login(request.Username, request.Password));
        }

        [HttpPost("UserExists")]
        public async Task<ActionResult<bool>> UserExists (UserDto request)
        {
            return Ok(await _authService.UserExists(request.Username));
        }
    }
}
