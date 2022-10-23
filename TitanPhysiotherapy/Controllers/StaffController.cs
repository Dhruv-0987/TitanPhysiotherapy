using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Models.StaffModel;
using TitanPhysiotherapy.Services.StaffService;

namespace TitanPhysiotherapy.Controllers
{
    [Route("api/Staff")]
    [ApiController]
    public class StaffController : ControllerBase
    {
        private readonly IStaffInterface _staffService;
        public StaffController(IStaffInterface staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("GetAllStaff")]
        public async Task<ActionResult<ServiceResponse<List<Staff>>>> GetAllStaff()
        {
            return Ok(await _staffService.GetAllStaff());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ServiceResponse<Staff>>> GetStaffById(int id)
        {
            return Ok(await _staffService.GetStaffById(id));
        }

        [HttpPost("AddStaff")]
        public async Task<ActionResult<ServiceResponse<List<Staff>>>> AddStaff(StaffDto staff)
        {
            return Ok(await _staffService.AddStaff(staff));
        }

        [HttpPut("UpdateStaff")]
        public async Task<ActionResult<ServiceResponse<List<Staff>>>> UpdateStaff(StaffDto newStaff)
        {
            return Ok(await _staffService.UpdateStaffById(newStaff));
        }

        [HttpPost("DeleteStaff/{staffId}")]
        public async Task<ActionResult<ServiceResponse<List<Staff>>>> DeleteStaff(int staffId)
        {
            return Ok(await _staffService.DeleteStaffById(staffId));
        }
    }
}
