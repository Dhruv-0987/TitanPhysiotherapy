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
using TitanPhysiotherapy.Models.PatientModels;
using TitanPhysiotherapy.Models.PatientModels.DTOS;
using TitanPhysiotherapy.Models.StaffModel;

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

        [HttpPost("RegisterPatient")]
        public async Task<ActionResult<ServiceResponse<Patient>>> RegisterPatient(PatientDto request)
        {
            return Ok(await _authService.RegisterPatient(request));
        }

        [HttpPost("LoginPatient")]
        public async Task<ActionResult<ServiceResponse<Patient>>> Login (PatientDto request)
        {
            return Ok(await _authService.LoginPatient(request.email, request.password));
        }

        [HttpPost("LoginStaff")]
        public async Task<ActionResult<ServiceResponse<Staff>>> LoginStaff(StaffDto request)
        {
            return Ok(await _authService.LoginPatient(request.email, request.password));
        }

        [HttpPost("UserExists")]
        public async Task<ActionResult<bool>> UserExists (UserDto request)
        {
            return Ok(await _authService.UserExists(request.Username));
        }
    }
}
