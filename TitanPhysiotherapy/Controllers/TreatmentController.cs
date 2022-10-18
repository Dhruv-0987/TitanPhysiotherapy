using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TitanPhysiotherapy.Models;
using TitanPhysiotherapy.Services.TreatmentService;

namespace TitanPhysiotherapy.Controllers
{
    [Route("api/Treatment")]
    [ApiController]
    public class TreatmentController : ControllerBase
    {
        private readonly ITreatmentInterface _treatmentService;

        public TreatmentController(ITreatmentInterface treatmentService)
        {
            _treatmentService = treatmentService;
        }

        [HttpGet("getTreatmentByPatientId/{patientId}")]
        public async Task<ActionResult<ServiceResponse<List<Treatment>>>> getTreatmentByPatientId(int patientId)
        {
            return Ok(_treatmentService.GetTreatmentByPatientId(patientId));
        }

        [HttpGet("getTreatmentByStaffId/{staffId}")]
        public async Task<ActionResult<ServiceResponse<Treatment>>> getTreatmentByStaffId(int staffId)
        {
            return Ok(await _treatmentService.GetTreatmentByStaffId(staffId));
        }

        [HttpGet("{treatmentId}")]
        public async Task<ActionResult<ServiceResponse<List<Treatment>>>> getTreatmentById(int id)
        {
            return Ok(await _treatmentService.GetTreatmentById(id));
        }
        
        [HttpPost("Add")]
        public async Task<ActionResult<ServiceResponse<List<Treatment>>>> addTreatment(TreatmentDto treatment)
        {
            return Ok(await _treatmentService.AddTreatment(treatment));
        }

        [HttpPut("Update")]
        public async Task<ActionResult<ServiceResponse<Treatment>>> updateTreatment(Treatment treatment)
        {
            return Ok(await _treatmentService.UpdateTreatment(treatment));    
        }

        [HttpDelete("Delete")]
        public async Task<ActionResult<ServiceResponse<Treatment>>> deleteTreatment(int id)
        {
            return Ok(await _treatmentService.RemoveTreatment(id));
        }

        [HttpGet("getTreatmentByStaffIdAndDate/{staffId}/{date}")]
        public async Task<ActionResult<ServiceResponse<List<Treatment>>>> getTreatmentByStaffIdAndDate(int staffId, string date)
        {
            return Ok(await _treatmentService.GetTreatmentsByStaffIdAndDate(staffId, date));
        }
    }
}
